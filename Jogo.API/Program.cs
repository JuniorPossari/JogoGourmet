using Jogo.Application;
using Jogo.Domain.Interfaces.Services;
using Jogo.Infrastructure;
using Jogo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = Host.CreateApplicationBuilder(args);

		var config = builder.Configuration;
		var services = builder.Services;

		//Configuration
		InfrastructureExtensions.ConfigureInfrastructure(services, config);
		ApplicationExtensions.ConfigureApplication(services, config);

		var host = builder.Build();

		//Update Database
		using (var dbScope = host.Services.CreateScope())
		{
			var context = dbScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
			context.Database.Migrate();
		}

		//Start
		using (var appScope = host.Services.CreateScope())
		{
			var jogoService = appScope.ServiceProvider.GetRequiredService<IJogoService>();
			jogoService.Iniciar();
		}
	}
}
