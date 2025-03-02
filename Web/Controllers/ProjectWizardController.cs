using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class ProjectWizardController : Controller
{
    private readonly IWizardService _wizardService;

    public ProjectWizardController(IWizardService projectService)
    {
        _wizardService = projectService;
    }

    public IActionResult FillProjectProps()
    {
        return View();
    }

    public IActionResult CreateCompanies()
    {
        return View();
    }
}