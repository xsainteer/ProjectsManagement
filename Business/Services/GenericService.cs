using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Business.Services;

public class GenericService<T> : IGenericService<T> where T : IHasId
{
    protected readonly IGenericRepository<T> _repository;
    protected readonly ILogger<GenericService<T>> _logger;

    
    public GenericService(IGenericRepository<T> repository, ILogger<GenericService<T>> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    
    public async Task<T?> GetByIdAsync(Guid id)
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

    
    public async Task<List<T>?> GetAllAsync(int skip, int count, bool asNoTracking = false, string? query = "")
    {
        try
        {
            if (query != null)
            {
                var entities = await _repository.GetAllAsync(skip, count, asNoTracking, query);
                if (entities.Count == 0)
                {
                    _logger.LogInformation("No {Entity} entities found", typeof(T).Name);
                    return entities;
                }
            }

            return null;
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

    
    public async Task DeleteAsync(Guid id)
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