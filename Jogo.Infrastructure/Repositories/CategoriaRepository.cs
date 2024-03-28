using Jogo.Domain.Entities;
using Jogo.Domain.Interfaces.Repositories;
using Jogo.Infrastructure.Data;

namespace Jogo.Infrastructure.Repositories
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationDbContext context) : base(context) { }

		public async Task<Categoria> ObterPorId(int idCategoria, string includeProperties = "", bool noTracking = true)
		{
			return await this.Get(x => x.Id == idCategoria, includeProperties: includeProperties, noTracking: noTracking);
		}

		public async Task<IList<Categoria>> ObterCategorias(int idCategoriaPai = 0, string includeProperties = "", bool noTracking = true)
        {
			if (idCategoriaPai > 0) return await this.List(x => x.IdCategoriaPai.HasValue && x.IdCategoriaPai == idCategoriaPai, includeProperties: includeProperties, noTracking: noTracking);

			return await this.List(x => !x.IdCategoriaPai.HasValue, noTracking: true);
        }

		public async Task<bool> PossuiSubCategorias(int idCategoria)
		{
			return await this.Any(x => x.Id == idCategoria && x.SubCategorias.Any());
		}
	}
}
