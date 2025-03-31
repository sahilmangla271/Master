using Inventory.Management.Infrastructure.Data.EF;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Inventory.Management.Infrastructure.Data
{

    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly InventoryDBContext _context;
        private readonly IRepository<T> _repository;
        private readonly DbSet<T> _dbSet;

        public Repository(InventoryDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<T> entity)
        {
            _dbSet.AddRange(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T?> GetAllByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            // Apply dynamic includes
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            // Get entity type
            var entityType = _context.Model.FindEntityType(typeof(T));
            var primaryKey = entityType?.FindPrimaryKey();

            if (primaryKey == null)
                throw new InvalidOperationException($"No primary key found for {typeof(T).Name}");

            var keyName = primaryKey.Properties.First().Name; // Correct way to get the property name

            // Fix: Use Reflection Instead of EF.Property<T>()
            var parameter = Expression.Parameter(typeof(T), "e");
            var property = Expression.Property(parameter, keyName);
            var equals = Expression.Equal(property, Expression.Constant(id));
            var lambda = Expression.Lambda<Func<T, bool>>(equals, parameter);

            return await query.FirstOrDefaultAsync(lambda);
        }
    }
}
