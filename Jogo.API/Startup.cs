using Jogo.Infrastructure;
using Microsoft.Extensions.Hosting;

namespace Jogo.API
{
	public static class Startup
	{
		public static void ConfigureAPI(string[] args)
		{
			var builder = Host.CreateApplicationBuilder(args);

			var config = builder.Configuration;
			var services = builder.Services;

			InfrastructureExtensions.ConfigureInfrastructure(services, config);

			var host = builder.Build();

			host.Run();
		}
	}
}
