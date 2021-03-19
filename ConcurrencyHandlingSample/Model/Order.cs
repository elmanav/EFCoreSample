using System;
using System.Collections.Generic;
using ConcurrencyHandlingSample.Model.Events;

namespace ConcurrencyHandlingSample.Model
{
	public class Order : AggregateRoot
	{
		public const int MaxOrderItemCount = 5;
		private List<OrderItem> _items;

		private DateTime? _modifyDate;

		public Order(Guid id)
		{
			Id = id;
			_items = new List<OrderItem>();
		}

		public Guid Id { get; }

		public void AddOrderItem(string productCode)
		{
			if (_items.Count >= MaxOrderItemCount) throw new InvalidOperationException("Order cannot have more than 5 order items.");

			_items.Add(OrderItem.CreateNew(productCode));
			_modifyDate = DateTime.UtcNow;
			AddDomainEvent(new OrderItemAddedDomainEvent(this));
		}
	}
}