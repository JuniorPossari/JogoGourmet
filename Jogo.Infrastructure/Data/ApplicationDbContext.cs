using Jogo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Jogo.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
		public ApplicationDbContext()
		{
		}

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<Categoria> Categoria { get; set; }

		public DbSet<Prato> Prato { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
			optionsBuilder.UseSqlite(configuration.GetConnectionString("Default"));
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Prato>(entity =>
			{
				entity.HasOne(d => d.Categoria).WithMany(p => p.Pratos).HasConstraintName("FK_Prato_Categoria");
			});
		}
    }
}
