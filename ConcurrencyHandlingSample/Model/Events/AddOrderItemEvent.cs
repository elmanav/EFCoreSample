namespace ConcurrencyHandlingSample.Model.Events
{
	public class OrderItemAddedDomainEvent : IDomainEvent
	{
		public OrderItemAddedDomainEvent(Order order)
		{
			Order = order;
		}

		public Order Order { get; }
	}
}