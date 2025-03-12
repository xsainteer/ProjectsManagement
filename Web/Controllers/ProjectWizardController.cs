using Business.Services;
using Domain.Entities;
using Domain.ViewModels;
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

    [HttpPost]
    public IActionResult FillProjectProps(Project project)
    {
        if (!ModelState.IsValid)
        {
            return View(project);
        }
        _wizardService.FillProjectProps(project);
        return RedirectToAction("FillCompanyProps");
    }

    public IActionResult FillCompanyProps()
    {
        return View();
    }

    [HttpPost]
    public IActionResult FillCompanyProps(Company clientCompany)
    {
        if (!ModelState.IsValid)
        {
            return View(clientCompany);
        }

        _wizardService.FillCompanyProps(clientCompany);

        return RedirectToAction("SelectSupervisorAndExecutors");
    }

    public IActionResult SelectSupervisorAndExecutors()
    {
        return View();
    }

    public JsonResult SearchEmployeesByQuery(string query)
    {
        return Json(_wizardService.GetEmployeesByQuery(query));
    }
}