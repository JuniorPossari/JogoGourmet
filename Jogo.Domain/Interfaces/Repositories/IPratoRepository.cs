using Jogo.Domain.Entities;

namespace Jogo.Domain.Interfaces.Repositories
{
    public interface IPratoRepository : IBaseRepository<Prato>
    {
		Task<IList<Prato>> ObterPorIdCategoria(int idCategoria);
	}
}
