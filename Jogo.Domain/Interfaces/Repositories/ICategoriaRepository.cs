using Jogo.Domain.Entities;

namespace Jogo.Domain.Interfaces.Repositories
{
    public interface ICategoriaRepository : IBaseRepository<Categoria>
    {
		Task<IList<Categoria>> ObterCategorias(int idCategoriaPai = 0);
		Task<bool> PossuiSubCategorias(int idCategoria);
	}
}
