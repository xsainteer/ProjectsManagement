using Database.Models;
using Database.Repositories;
using Microsoft.Extensions.Logging;

namespace Business.Services;

public interface IProjectService
{
    public Task<Project> GetProjectByIdAsync(int id);
    public Task<List<Project>> GetAllProjectsAsync();
    public Task AddProjectAsync(Project project);
    public Task UpdateProjectAsync(int id, Project project);
    public Task DeleteProjectAsync(int id);
}
public class ProjectService
{
    private readonly IGenericRepository<Project> _repository;
    private readonly ILogger<ProjectService> _logger;

    
    public ProjectService(IGenericRepository<Project> repository, ILogger<ProjectService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    
    public async Task<Project> GetProjectByIdAsync(int id)
    {
        try
        {
            return await _repository.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while getting project by id {id}: {ex.Message}");
            throw;
        }
    }

    
    public async Task<List<Project>> GetAllProjectsAsync()
    {
        try
        {
            var projects = await _repository.GetAllAsync();
            if (projects.Count == 0)
            {
                _logger.LogInformation("No projects found");
            }
            return projects;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while getting all projects: {ex.Message}");
            throw;
        }
    }

    
    public async Task AddProjectAsync(Project project)
    {
        try
        {
            //TODO
            //some logics to be added

            await _repository.AddAsync(project);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while creating project: {ex.Message}");
            throw;
        }
    }

    
    public async Task UpdateProjectAsync(int id, Project project)
    {
        //TODO
        try
        {
            await _repository.UpdateAsync(project);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while updating project: {ex.Message}");
            throw;
        }
    }

    
    public async Task DeleteProjectAsync(int id)
    {
        try
        {
            await _repository.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while deleting project: {ex.Message}");
            throw;
        }
    }
}