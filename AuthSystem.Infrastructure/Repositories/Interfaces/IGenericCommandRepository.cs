using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;

namespace Kwlc.Infrastructure.Repositories.Interfaces
{
    public interface IGenericCommandRepository<T>
    {
        Task<T> InsertAndRetrieveIdAsync(T entity);
        Task<int> InsertAsync(T entity);
        Task<int> InsertRangeAsync(List<T> entities);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(T entity);
        Task<int> DeleteRangeAsync(List<T> entities);
        Task<int> UpdateRangeAsync(List<T> entities);
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync();
    }
}
