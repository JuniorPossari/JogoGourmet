using Jogo.Domain.Entities;
using System.Linq.Expressions;

namespace Jogo.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            bool noTracking = false,
            CancellationToken cancellationToken = default
        );

        Task<IList<T>> List(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            bool noTracking = false,
            CancellationToken cancellationToken = default
        );

        Task<bool> Any(
            Expression<Func<T, bool>> filter = null,
            CancellationToken cancellationToken = default
        );

        Task<IList<T>> Filter(
            IQueryable<T> query,
            int initialPosition,
            int itensPerPage,
            string includeProperties = "",
            CancellationToken cancellationToken = default
        );

        Task<IList<TEntity>> Filter<TEntity>(
            Expression<Func<T, TEntity>> keySelector,
            IQueryable<T> query,
            int initialPosition,
            int itensPerPage,
            CancellationToken cancellationToken = default
        );

        bool Add(T entity);

		bool Update(T entity);

		bool Remove(T entity);

		bool Remove(Expression<Func<T, bool>> filter);
    }
}
