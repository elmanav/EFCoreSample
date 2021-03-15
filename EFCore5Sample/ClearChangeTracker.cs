using System;
using System.Threading.Tasks;
using TestSupport.EfHelpers;

namespace EFCoreSample
{
	internal class ClearChangeTracker
	{
		private static async Task Main()
		{
			var options = SqliteInMemory.CreateOptionsWithLogTo<SampleDbContext>(Console.WriteLine);
			await using var context = new SampleDbContext(options);
			await context.Database.EnsureCreatedAsync();

			var craft = new Aircraft("Boeing 737");
			var id = craft.Id;
			context.Aircrafts.Add(craft);
			await context.SaveChangesAsync();

			var c1 = await context.FindAsync<Aircraft>(id);
			context.ChangeTracker.Clear();
			var c2 = await context.FindAsync<Aircraft>(id);
			Console.WriteLine(ReferenceEquals(c1, c2));
			Console.WriteLine(c1 == c2);
		}
	}
}