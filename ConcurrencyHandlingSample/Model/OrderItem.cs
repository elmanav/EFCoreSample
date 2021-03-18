using System;

namespace ConcurrencyHandlingSample.Model
{
	public class OrderItem
	{
		private OrderItem()
		{
		}

		private OrderItem(string productCode)
		{
			Id = Guid.NewGuid();

			ProductCode = productCode;
		}

		public Guid Id { get; }

		public string ProductCode { get; }

		internal static OrderItem CreateNew(string productCode)
		{
			return new OrderItem(productCode);
		}
	}
}