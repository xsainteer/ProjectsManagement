using Business.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IGenericService<Project> _projectService;
    private readonly ILogger<ProjectsController> _logger;

    public ProjectsController(IGenericService<Project> projectService, ILogger<ProjectsController> logger)
    {
        _projectService = projectService;
        _logger = logger;
    }

    // there is no reason to use DTO when creating a project
    // because we are not sending any data to the client and every property of the
    // frontend project is already in the project entity
    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] Project project)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _projectService.AddAsync(project);
        }
        catch (Exception e)
        {
            _logger.LogError("Error while creating project: {ErrorMessage}", e.Message);
            return StatusCode(500, new {message = "An error occurred while creating the project"});
        }
        
        return Ok();
    }
}