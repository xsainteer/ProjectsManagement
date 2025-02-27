using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class ProjectWizardController : Controller
{
    public IProjectService _projectService { get; set; }

    public ProjectWizardController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    public IActionResult Step1()
    {
        return View();
    }
}