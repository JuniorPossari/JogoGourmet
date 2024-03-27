using Jogo.Application.Services;
using Jogo.Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jogo.Application
{
	public static class ApplicationExtensions
	{
		public static void ConfigureApplication(this IServiceCollection services, IConfiguration config)
		{
			services.AddSingleton<IJogoService, JogoService>();
		}
	}
}
