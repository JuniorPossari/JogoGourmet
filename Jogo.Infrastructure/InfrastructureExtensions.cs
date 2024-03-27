using Jogo.Domain.Interfaces.Repositories;
using Jogo.Infrastructure.Data;
using Jogo.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jogo.Infrastructure
{
	public static class InfrastructureExtensions
	{
		public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration config)
		{
			var connectionString = config.GetConnectionString("Default");

			services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlite(connectionString));

			services.AddScoped<ICategoriaRepository, CategoriaRepository>();
		}
	}
}
