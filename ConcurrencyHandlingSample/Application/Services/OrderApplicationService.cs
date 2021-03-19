using System;
using System.Threading;
using System.Threading.Tasks;
using ConcurrencyHandlingSample.Data;
using Microsoft.Extensions.Logging;

namespace ConcurrencyHandlingSample.Application.Services
{
	public class OrderApplicationService
	{
		private readonly OrderDbContext _context;
		private readonly EventDispatcher _dispatcher;
		private readonly ILogger _logger;

		public OrderApplicationService(OrderDbContext context, EventDispatcher dispatcher, ILogger logger)
		{
			_context = context;
			_dispatcher = dispatcher;
			_logger = logger;
		}

		public async Task AddOrderItemAsync(Guid orderId,
			string itemCode, CancellationToken cancellationToken)
		{
			var order = await _context.Orders.FindAsync(orderId);
			await Task.Delay(1000, cancellationToken);
			order.AddOrderItem(itemCode);
			_dispatcher.Dispatch(order.DomainEvents);
			await _context.SaveChangesAsync(cancellationToken);
			_logger.LogInformation("Added item {itemCode} to order {@order}.", itemCode, order);
		}
	}
}