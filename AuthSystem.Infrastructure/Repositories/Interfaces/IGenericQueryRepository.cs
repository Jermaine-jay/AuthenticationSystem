using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Kwlc.Infrastructure.Repositories.Interfaces
{
    public interface IGenericQueryRepository<T>
    {
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task<IQueryable<T>> GetPaginatedAsync();
        Task<IQueryable<T>> GetPaginatedAsync(Expression<Func<T, bool>> predicate);
        Task<IQueryable<T>> GetAllWithIncludeAsync(Expression<Func<T, bool>>? predicate, params string[] includes);

        Task<T> GetSingleAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool track = true);
    }
}
