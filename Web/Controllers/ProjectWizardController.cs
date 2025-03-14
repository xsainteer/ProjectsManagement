using Business.Services;
using Domain.Entities;
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
        _wizardService.SaveProjectProps(project);
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

        _wizardService.SaveCompanyProps(clientCompany);

        return RedirectToAction("SelectSupervisorAndExecutors");
    }

    public IActionResult SelectSupervisorAndExecutors()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SelectSupervisorAndExecutors(string jsonSupervisor, string jsonExecutors)
    {
        _wizardService.SaveSupervisorAndExecutors(jsonSupervisor, jsonExecutors);
        return RedirectToAction("SelectSupervisorAndExecutors");
    }
    
    
    

    public JsonResult SearchEmployeesByQuery(string query)
    {
        return Json(_wizardService.GetEmployeesByQuery(query));
    }
}