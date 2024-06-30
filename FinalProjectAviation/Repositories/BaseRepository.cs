
using FinalProjectAviation.Data;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectAviation.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly AviationDBDbContext _context;
        private readonly DbSet<T> _dbSet;   

        public BaseRepository(AviationDBDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task AddAsync(T item)
        {
            await _dbSet.AddAsync(item);
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> items)
        {
            await _context.AddRangeAsync(items);
        }

        public async Task<int> CountAsync()
        {
            var count = await _dbSet.CountAsync();
            return count;
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            T? existing = await _dbSet.FindAsync(id);
            if (existing != null)
            {
                _dbSet.Remove(existing);
                return true;
            }
            return false;

        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _dbSet.ToListAsync();
            return entities;
        }

        public virtual async Task<T?> GetAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity;
        }

        public virtual void UpdateAsync(T item)
        {
            _dbSet.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
