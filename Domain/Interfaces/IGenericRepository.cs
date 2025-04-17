namespace Domain.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync(int skip, int count, bool asNoTracking = false, string query = "");
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    Task UpdateAsync(T entity);
    Task UpdateChangedFieldsAsync(T entity);
    Task DeleteAsync(int id);
    Task SaveChangesAsync();
}