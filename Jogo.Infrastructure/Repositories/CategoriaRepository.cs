using Jogo.Domain.Entities;
using Jogo.Domain.Interfaces.Repositories;
using Jogo.Infrastructure.Data;

namespace Jogo.Infrastructure.Repositories
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationDbContext context) : base(context) { }
    }
}
