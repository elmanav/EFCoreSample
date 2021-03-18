using ConcurrencyHandlingSample.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConcurrencyHandlingSample.Data
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.ToTable("Orders", "orders");
			builder.HasKey(order => order.Id);
			builder.Property(order => order.Id).ValueGeneratedNever();
			builder.OwnsMany<OrderItem>("_items", item =>
			{
				item.WithOwner().HasForeignKey("OrderId");

				item.ToTable("OrderItems", "orders");

				item.HasKey(i => i.Id);
				item.Property(orderItem => orderItem.Id).ValueGeneratedNever();
				item.Property(i => i.ProductCode).HasColumnName("ProductCode");
			});
		}
	}
}