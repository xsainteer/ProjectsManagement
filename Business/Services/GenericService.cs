using Data.Repositories;
using Microsoft.Extensions.Logging;

namespace Business.Services;

public interface IGenericService<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}

public class GenericService<T> : IGenericService<T> where T : class
{
    protected readonly IGenericRepository<T> _repository;
    protected readonly ILogger<GenericService<T>> _logger;

    
    public GenericService(IGenericRepository<T> repository, ILogger<GenericService<T>> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    
    public async Task<T?> GetByIdAsync(int id)
    {
        try
        {
            return await _repository.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while getting {Entity}:{Id}: {ErrorMessage}", typeof(T).Name, id, ex.Message);
            throw;
        }
    }

    
    public async Task<List<T>> GetAllAsync()
    {
        try
        {
            var entities = await _repository.GetAllAsync();
            if (entities.Count == 0)
            {
                _logger.LogInformation("No {Entity} entities found", typeof(T).Name);
            }
            return entities;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while getting all {Entity} entities: {ErrorMessage}", typeof(T).Name, ex.Message);
            throw;
        }
    }

    
    public async Task AddAsync(T entity)
    {
        try
        {
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while creating {Entity}: {ErrorMessage}", typeof(T).Name, ex.Message);
            throw;
        }
    }

    
    public async Task UpdateAsync(T entity)
    {
        try
        {
            await _repository.UpdateAsync(entity);
            await _repository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while updating {Entity}: {ErrorMessage}", typeof(T).Name, ex.Message);
            throw;
        }
    }

    
    public async Task DeleteAsync(int id)
    {
        try
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while deleting project: {ErrorMessage}", ex.Message);
            throw;
        }
    }
}