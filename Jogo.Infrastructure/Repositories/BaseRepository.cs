using Jogo.Domain.Entities;
using Jogo.Domain.Interfaces.Repositories;
using Jogo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jogo.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        private static readonly char[] _separator = [','];

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            bool noTracking = false,
            CancellationToken cancellationToken = default
        )
        {
            var query = _context.Set<T>().AsQueryable();

            if (noTracking) query = query.AsNoTracking();

            if (filter != null) query = query.Where(filter);

            if (orderBy != null) query = orderBy(query);

            foreach (var includeProperty in includeProperties.Split(_separator, StringSplitOptions.RemoveEmptyEntries)) query = query.Include(includeProperty);

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IList<T>> List(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            bool noTracking = false,
            CancellationToken cancellationToken = default
        )
        {
            var query = _context.Set<T>().AsQueryable();

            if (noTracking) query = query.AsNoTracking();

            if (filter != null) query = query.Where(filter);

            if (orderBy != null) query = orderBy(query);

            foreach (var includeProperty in includeProperties.Split(_separator, StringSplitOptions.RemoveEmptyEntries)) query = query.Include(includeProperty);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<IList<TEntity>> Select<TEntity>(
            Expression<Func<T, TEntity>> keySelector,
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            bool noTracking = false,
            CancellationToken cancellationToken = default
        )
        {
            var query = _context.Set<T>().AsQueryable();

            if (noTracking) query = query.AsNoTracking();

            if (filter != null) query = query.Where(filter);

            if (orderBy != null) query = orderBy(query);

            foreach (var includeProperty in includeProperties.Split(_separator, StringSplitOptions.RemoveEmptyEntries)) query = query.Include(includeProperty);

            return await query.Select(keySelector).ToListAsync(cancellationToken);
        }

        public async Task<bool> Any(
            Expression<Func<T, bool>> filter = null, 
            CancellationToken cancellationToken = default
        )
        {
            var query = _context.Set<T>().AsNoTracking();

            if (filter != null) return await query.AnyAsync(filter, cancellationToken);

            return await query.AnyAsync(cancellationToken);
        }

        public async Task<IList<T>> Filter(
            IQueryable<T> query,
            int initialPosition,
            int itensPerPage,
            string includeProperties = "",
            CancellationToken cancellationToken = default
        )
        {
            foreach (var includeProperty in includeProperties.Split(_separator, StringSplitOptions.RemoveEmptyEntries)) query = query.Include(includeProperty);

            return await query.AsNoTracking().Skip(initialPosition).Take(itensPerPage).ToListAsync(cancellationToken);
        }

        public async Task<IList<TEntity>> Filter<TEntity>(
            Expression<Func<T, TEntity>> keySelector,
            IQueryable<T> query,
            int initialPosition,
            int itensPerPage,
            CancellationToken cancellationToken = default
        )
        {
            return await query.AsNoTracking().Skip(initialPosition).Take(itensPerPage).Select(keySelector).ToListAsync(cancellationToken);
        }

        public void Add(T entity)
        {
            entity.DateAdded = DateTime.Now;
            _context.Add(entity);
        }

        public void Update(T entity)
        {
            entity.DateUpdated = DateTime.Now;
            _context.Update(entity);
        }

        public void Remove(T entity)
        {
            _context.Remove(entity);
        }

        public void Remove(Expression<Func<T, bool>> filter)
        {
            _context.Remove(filter);
        }
    }
}
