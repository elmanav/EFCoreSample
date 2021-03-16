using EFCoreSample.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreSample.Configurations
{
	public class AircraftTypeConfiguration : EntityConfiguration<AircraftType>
	{
		/// <inheritdoc />
		public override void Configure(EntityTypeBuilder<AircraftType> builder)
		{
			base.Configure(builder);
			builder.ToTable("AircraftTypes");
			builder.Property(type => type.Name);
			builder.IndexerProperty<string>("Vendor");
			builder.IndexerProperty<int>("MaxTOW");
			builder.IndexerProperty<float>("MaxFuel");
		}
	}
}