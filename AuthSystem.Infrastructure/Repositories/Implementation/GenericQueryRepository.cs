using AuthSystem.Infrastructure.Context;
using Kwlc.Infrastructure.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace AuthSystem.Infrastructure.Repositories.Implementation
{
    public class GenericQueryRepository<T> : IGenericQueryRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        public GenericQueryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

       
        public async Task<IQueryable<T>> GetPaginatedAsync(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate).AsQueryable();
        }
        public async Task<IQueryable<T>> GetPaginatedAsync()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AnyAsync(predicate);
        }

        public async Task<IQueryable<T>> GetAllWithIncludeAsync(Expression<Func<T, bool>>? predicate, params string[] includes)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (predicate != null)
            {
                query.Where(predicate);
            }

            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }

        public async Task<T> GetSingleAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool track = true)
        {
            var query = _dbContext.Set<T>()
                .Where(predicate);

            query = track ? query.AsNoTracking() : query;

            if (include != null) query = include(query);

            return await query.FirstOrDefaultAsync();
        }
    }
}
