using Jogo.Domain.Entities;
using Jogo.Domain.Interfaces.Repositories;
using Jogo.Infrastructure.Data;

namespace Jogo.Infrastructure.Repositories
{
    public class PratoRepository : BaseRepository<Prato>, IPratoRepository
    {
        public PratoRepository(ApplicationDbContext context) : base(context) { }

		public async Task<IList<Prato>> ObterPorIdCategoria(int idCategoria)
		{
			return await this.List(x => x.IdCategoria == idCategoria, noTracking: true);
		}
	}
}
