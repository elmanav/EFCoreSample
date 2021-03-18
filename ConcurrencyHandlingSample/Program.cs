using System;
using System.Linq;
using System.Threading.Tasks;
using ConcurrencyHandlingSample.Data;
using ConcurrencyHandlingSample.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConcurrencyHandlingSample
{
	internal class Program
	{
		private static async Task Main(string[] args)
		{
			var orderId = new Guid("33619B0B-8E60-443D-A90A-95EFD48F49C2");
			var order = new Order(orderId);
			var builder = new DbContextOptionsBuilder<OrderDbContext>();
			builder.UseSqlite("DataSource=test.dat");
			builder.LogTo(Console.WriteLine, LogLevel.Information);
			var options = builder.Options;
			await Create(options, order);
			var requestTasks = Enumerable.Range(1, 6)
				.Select(idx => AddOrderItemAsync(options, orderId, $"Product_{idx}"));
			await Task.WhenAll(requestTasks);
		}

		private static async Task Create(DbContextOptions<OrderDbContext> options, Order order)
		{
			await using var context = new OrderDbContext(options);
			await context.Database.EnsureDeletedAsync();
			await context.Database.EnsureCreatedAsync();
			await context.Orders.AddAsync(order);
			await context.SaveChangesAsync();
		}

		private static async Task AddOrderItemAsync(DbContextOptions<OrderDbContext> options, Guid orderId,
			string itemCode)
		{
			await using var context = new OrderDbContext(options);
			var order = await context.Orders.FindAsync(orderId);
			await Task.Delay(1000);
			order.AddOrderLine(itemCode);
			await context.SaveChangesAsync();
			Console.WriteLine($"Added item {itemCode} to order.");
		}
	}
}