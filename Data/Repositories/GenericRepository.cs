using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;
    private readonly ILogger<GenericRepository<T>> _logger;

    public GenericRepository(AppDbContext context, ILogger<GenericRepository<T>> logger)
    {
        _context = context;
        _dbSet = context.Set<T>();
        _logger = logger;
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Fetching {Entity}:{Id}", typeof(T).Name, id);
        return await _dbSet.FindAsync(id);
    }

    public async Task<List<T>> GetAllAsync(bool asNoTracking = false)
    {
        _logger.LogInformation("Fetching all {Entity} records", typeof(T).Name);
        return asNoTracking
            ? await _dbSet.AsNoTracking().ToListAsync()
            : await _dbSet.ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        var entityId = _context.Entry(entity).Property("Id").CurrentValue;
        _logger.LogInformation("Adding new {Entity}:{Id}", typeof(T).Name, entityId);
        await _dbSet.AddAsync(entity);
    }
    
    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        _logger.LogInformation("Adding new {Entity} records", typeof(T).Name);
        await _dbSet.AddRangeAsync(entities);
    }

    public async Task UpdateAsync(T entity)
    {
        _logger.LogInformation("Updating {Entity}", typeof(T).Name);

        var entry = _context.Entry(entity);
        var entityId = entry.Property("id").CurrentValue;
        if (entry.State == EntityState.Detached)
        {
            _logger.LogWarning("Entity {Entity}:{Id} is detached. Attaching...", typeof(T).Name, entityId);
            _dbSet.Attach(entity);
        }

        entry.State = EntityState.Modified;
    }

    public async Task UpdateChangedFieldsAsync(T entity)
    {

        var entry = _context.Entry(entity);
        
        var entityId = entry.Property("Id").CurrentValue;
        
        _logger.LogInformation("Updating changed fields for {Entity}:{Id}", typeof(T).Name, entityId);
        
        if (entry.State == EntityState.Detached)
        {
            _logger.LogWarning("Entity {Entity}:{Id} is detached. Attaching...", typeof(T).Name, entityId);
            _dbSet.Attach(entity);
        }

        foreach (var property in entry.Properties)
        {
            if (!property.IsModified && !Equals(property.OriginalValue, property.CurrentValue))
            {
                property.IsModified = true;
                _logger.LogInformation("Modified property {Property} for {Entity}:{Id}", property.Metadata.Name, typeof(T).Name, entityId);
            }
        }
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
        {
            _logger.LogWarning("Entity {Entity}:{Id} not found for deletion", typeof(T).Name, id);
            return;
        }

        _logger.LogInformation("Deleting {Entity}:{Id}", typeof(T).Name, id);
        _dbSet.Remove(entity);
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}