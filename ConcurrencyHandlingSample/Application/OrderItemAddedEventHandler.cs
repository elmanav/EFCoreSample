using ConcurrencyHandlingSample.Model.Events;

namespace ConcurrencyHandlingSample.Application
{
	public class OrderItemAddedEventHandler : IDomainEventHandler<OrderItemAddedDomainEvent>
	{
		public void Handle(OrderItemAddedDomainEvent @event)
		{
			var order = @event.Order;
			order.IncreaseVersion();
		}
	}
}