using Data.Repositories;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Business.Services;

public interface IProjectService : IGenericService<Project>
{
    
}
public class ProjectService : GenericService<Project>, IProjectService
{
    private readonly ILogger<ProjectService> _logger;
    public ProjectService(IGenericRepository<Project> repository, ILogger<ProjectService> logger) : base(repository, logger)
    {
        _logger = logger;
    }
}