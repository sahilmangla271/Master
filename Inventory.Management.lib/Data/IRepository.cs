using System.Linq.Expressions;

namespace Inventory.Management.Infrastructure.Data
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task DeleteAsync(int id);
        Task<T?> GetAllByIdAsync(int id, params Expression<Func<T, object>>[] includes);

    }
}
