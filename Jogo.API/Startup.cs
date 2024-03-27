using Jogo.Infrastructure;
using Jogo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

			using (var scope = host.Services.CreateScope())
			{
				var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
				context.Database.Migrate();
			}

			Console.Clear();
			Console.WriteLine("Aperte uma tecla para continuar...");	
			Console.ReadKey();
		}
	}
}
