using Jogo.Domain.Entities;

namespace Jogo.Domain.Interfaces.Repositories
{
    public interface ICategoriaRepository : IBaseRepository<Categoria>
    {
		Task<Categoria> ObterPorId(int idCategoria, string includeProperties = "", bool noTracking = true);
		Task<IList<Categoria>> ObterCategorias(int idCategoriaPai = 0, string includeProperties = "", bool noTracking = true);
		Task<bool> PossuiSubCategorias(int idCategoria);
	}
}
