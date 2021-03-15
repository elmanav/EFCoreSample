using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreSample.Configurations
{
	public abstract class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity
	{
		/// <inheritdoc />
		public virtual void Configure(EntityTypeBuilder<TEntity> builder)
		{
			builder.Property(project => project.Id)
				.IsRequired()
				.HasColumnName("_Id")
				.ValueGeneratedNever();

			builder.HasKey(entity => entity.Id);
		}
	}
}