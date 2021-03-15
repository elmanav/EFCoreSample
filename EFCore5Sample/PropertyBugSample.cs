using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestSupport.EfHelpers;

namespace EFCoreSample
{
	internal class PropertyBugSample
	{
		private static async Task Main()
		{
			var options = SqliteInMemory.CreateOptionsWithLogTo<SampleDbContext>(Console.WriteLine);
			await using var context = new SampleDbContext(options);
			await context.Database.EnsureCreatedAsync();
			var a1 = new Dictionary<string, object>
			{
				["Id"] = Guid.NewGuid(),
				["Name"] = "Vnukovo Andrei Tupolev International Airport",
				["ICAO"] = "UUWW"
			};

			context.Airports.Add(a1);
			await context.SaveChangesAsync();
			context.ChangeTracker.Clear();
			var a2 = await context.Airports.SingleAsync(e => (string) e["ICAO"] == "UUWW");
			Console.WriteLine((string) a1["ICAO"] == (string) a2["ICAO"]);
		}
	}
}