using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ConcurrencyHandlingSample.Application;
using ConcurrencyHandlingSample.Application.Services;
using ConcurrencyHandlingSample.Data;
using ConcurrencyHandlingSample.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;

namespace ConcurrencyHandlingSample
{
	public class ClientRunner : IHostedService
	{
		private readonly EventDispatcher _dispatcher;
		private readonly ILogger<ClientRunner> _logger;

		public ClientRunner(EventDispatcher dispatcher, ILogger<ClientRunner> logger)
		{
			_dispatcher = dispatcher;
			_logger = logger;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			var orderId = Guid.NewGuid();
			var order = new Order(orderId);
			var builder = new DbContextOptionsBuilder<OrderDbContext>();
			builder.UseSqlite("DataSource=test.dat");
			builder.LogTo(Console.WriteLine, LogLevel.Warning);
			builder.EnableSensitiveDataLogging();

			var retryPolicy = Policy.Handle<DbUpdateConcurrencyException>()
				.WaitAndRetryForeverAsync(i => TimeSpan.FromSeconds(i));

			var options = builder.Options;
			await Create(options, order);

			var requestTasks = Enumerable.Range(1, Order.MaxOrderItemCount + 1)
				.Select(idx =>
				{
					return retryPolicy.ExecuteAsync((token) =>
						ClientAddOrderItemAsync(options, orderId, $"Product_{idx}", token), cancellationToken);
				});

			try
			{
				await Task.WhenAll(requestTasks);
			}
			catch (InvalidOperationException exc)
			{
				_logger.LogError(exc, "Client request error");
			}
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}

		private async Task Create(DbContextOptions<OrderDbContext> options, Order order)
		{
			await using var context = new OrderDbContext(options);
			await context.Database.EnsureDeletedAsync();
			await context.Database.EnsureCreatedAsync();
			await context.Orders.AddAsync(order);
			await context.SaveChangesAsync();
		}

		private async Task ClientAddOrderItemAsync(DbContextOptions<OrderDbContext> options, Guid orderId,
			string itemCode, CancellationToken cancellationToken)
		{
			await using var context = new OrderDbContext(options);
			var service = new OrderApplicationService(context, _dispatcher, _logger);
			await service.AddOrderItemAsync(orderId, itemCode, cancellationToken);
		}
	}
}