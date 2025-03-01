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

    //the whole wizard frontend implementation
    public IActionResult CreateProject()
    {
        return View();
    }
    
    
}