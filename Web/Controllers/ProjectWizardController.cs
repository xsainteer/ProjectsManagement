using Business.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class ProjectWizardController : Controller
{
    private readonly IProjectService _projectService;

    public ProjectWizardController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    //first step of creating a project
    [HttpGet]
    public Task AddProject(Project project)
    {
        return _projectService.AddProjectAsync(project);
    }
    public IActionResult CreateProject()
    {
        return View();
    }
    
    
}