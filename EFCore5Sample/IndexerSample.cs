using System;
using System.Threading.Tasks;
using EFCoreSample.Entities;
using Microsoft.EntityFrameworkCore;
using TestSupport.EfHelpers;

namespace EFCoreSample
{
	public class IndexerSample
	{
		static async Task Main(string[] args)
		{
			var options = SqliteInMemory.CreateOptionsWithLogTo<SampleDbContext>(Console.WriteLine);
			await using var context = new SampleDbContext(options);
			await context.Database.EnsureCreatedAsync();
			var type = new AircraftType("Airbus 321") {["Vendor"] = "Airbus Co", ["MaxTOW"] = 40000, ["MaxFuel"] = 2000.0F};
			var id = type.Id;
			await context.AircraftTypes.AddAsync(type);
			await context.SaveChangesAsync();
			context.ChangeTracker.Clear();
			var retType = await context.AircraftTypes.SingleAsync(t => ((string)t["Vendor"]).Contains("Airbus"));
			Console.WriteLine(retType);
		}
	}
}