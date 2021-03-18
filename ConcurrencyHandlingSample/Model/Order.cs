using System;
using System.Collections.Generic;

namespace ConcurrencyHandlingSample.Model
{
	public class Order
	{
		private List<OrderItem> _items;

		private DateTime? _modifyDate;

		public Order(Guid id)
		{
			Id = id;
			_items = new List<OrderItem>();
		}

		public Guid Id { get; }

		public void AddOrderLine(string productCode)
		{
			if (_items.Count >= 5) throw new Exception("Order cannot have more than 5 order items.");

			_items.Add(OrderItem.CreateNew(productCode));
			_modifyDate = DateTime.Now;
		}
	}
}