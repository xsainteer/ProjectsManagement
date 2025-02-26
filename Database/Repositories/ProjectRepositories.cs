using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Database.Repositories;

public interface IProjectRepository
{
    Task<Project> GetByIdAsync(int id);
    Task<List<Project>> GetAllAsync();
    Task CreateAsync(Project project);
    Task UpdateAsync(int id, Project project);
    Task DeleteAsync(int id);
}

public class ProjectRepository : IProjectRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<ProjectRepository> _logger;

    public ProjectRepository(AppDbContext context, ILogger<ProjectRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    
    public async Task<Project> GetByIdAsync(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null)
        {
            _logger.LogError($"Requested Project with id: {id} was not found");
            throw new KeyNotFoundException($"Project with id {id} not found");
        }
        return project;
    }

    
    public async Task<List<Project>> GetAllAsync()
    {
        return await _context.Projects.ToListAsync();
    }

    
    public async Task CreateAsync(Project project)
    {
        try
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while creating project: {ex.Message}");
            throw new InvalidOperationException("An error occurred while creating the project.");
        }
    }

    
    public async Task UpdateAsync(int id, Project updatedPoject)
    {
        try
        {
            var project = GetByIdAsync(id);
                //TODO
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while updating project: {ex.Message}");
            throw new InvalidOperationException("An error occurred while updating the project.");
        }
    }

    
    public async Task DeleteAsync(int id)
    {
        try
        {
            var project = await GetByIdAsync(id);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while deleting project: {ex.Message}");
            throw new InvalidOperationException("An error occurred while deleting the project.");
        }
    }
}