using System.Threading.Tasks;
using ConcurrencyHandlingSample.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ConcurrencyHandlingSample
{
	internal class Program
	{
		private static async Task Main()
		{
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.Enrich.FromLogContext()
				.WriteTo.Console()
				.CreateLogger();

			var hostBuilder = new HostBuilder()
				.ConfigureLogging(loggingBuilder => loggingBuilder.AddSerilog())
				.ConfigureServices(services =>
				{
					services.AddHostedService<ClientRunner>();
					services.AddSingleton<EventDispatcher>();
				});
			await hostBuilder.RunConsoleAsync();
		}
	}
}