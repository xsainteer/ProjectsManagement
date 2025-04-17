namespace Domain.Interfaces;

public interface IGenericService<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync(int skip, int count, bool asNoTracking = false, string? query = "");
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}