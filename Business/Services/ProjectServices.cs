using Data.Repositories;
using Domain.Entities;
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
public class ProjectService : IProjectService
{
    private readonly IGenericRepository<Project> _projectRepository;
    private readonly ILogger<ProjectService> _logger;

    
    public ProjectService(IGenericRepository<Project> projectRepository, ILogger<ProjectService> logger)
    {
        _projectRepository = projectRepository;
        _logger = logger;
    }

    
    public async Task<Project> GetProjectByIdAsync(int id)
    {
        try
        {
            return await _projectRepository.GetByIdAsync(id);
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
            var projects = await _projectRepository.GetAllAsync();
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
            await _projectRepository.AddAsync(project);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while creating project: {ErrorMessage}", ex.Message);
            throw;
        }
    }

    
    public async Task UpdateProjectAsync(int id, Project project)
    {
        //TODO
        try
        {
            await _projectRepository.Update(project);
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
            await _projectRepository.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while deleting project: {ex.Message}");
            throw;
        }
    }
}