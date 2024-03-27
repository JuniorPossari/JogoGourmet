using Jogo.Domain.Entities;
using Jogo.Domain.Interfaces.Repositories;
using Jogo.Infrastructure.Data;

namespace Jogo.Infrastructure.Repositories
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IList<Categoria>> ObterCategorias(int idCategoriaPai = 0)
        {
			if (idCategoriaPai > 0) return await this.List(x => x.IdCategoriaPai.HasValue && x.IdCategoriaPai == idCategoriaPai, noTracking: true);

			return await this.List(x => !x.IdCategoriaPai.HasValue, noTracking: true);
        }

		public async Task<bool> PossuiSubCategorias(int idCategoria)
		{
			return await this.Any(x => x.Id == idCategoria && x.SubCategorias.Any());
		}
	}
}
