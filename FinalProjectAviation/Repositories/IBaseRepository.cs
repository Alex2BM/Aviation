namespace FinalProjectAviation.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync(); 
        Task<T?> GetAsync(int id);  
        Task AddAsync(T item);
        void UpdateAsync(T item);  
        Task AddRangeAsync(IEnumerable<T> items);
        Task<bool> DeleteAsync(int id);
        Task<int> CountAsync();
    }
}
 