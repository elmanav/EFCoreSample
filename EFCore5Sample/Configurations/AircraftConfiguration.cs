using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreSample.Configurations
{
	public class AircraftConfiguration : EntityConfiguration<Aircraft>
	{
		/// <inheritdoc />
		public override void Configure(EntityTypeBuilder<Aircraft> builder)
		{
			base.Configure(builder);
			builder.ToTable("Aircrafts");
			builder.Property(aircraft => aircraft.Name).HasColumnName("Name");
		}
	}
}