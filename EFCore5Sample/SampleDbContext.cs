using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace EFCoreSample
{
	public class SampleDbContext : DbContext
	{
		public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options)
		{
		}

		public DbSet<Aircraft> Aircrafts => Set<Aircraft>();

		public DbSet<Dictionary<string, object>> Airports => Set<Dictionary<string, object>>("Airport");

		/// <inheritdoc />
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			modelBuilder.SharedTypeEntity<Dictionary<string, object>>("Airport", b =>
			{
				b.IndexerProperty<Guid>("Id");
				b.IndexerProperty<string>("Name").IsRequired();
				b.IndexerProperty<string>("ICAO");
			});
		}
	}
}