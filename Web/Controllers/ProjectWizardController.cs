using Business.Services;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

//TODO need to add a feature for going to a previous step with all fields' values saved
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
        return RedirectToAction("FillClientCompanyProps");
    }

    public IActionResult FillClientCompanyProps()
    {
        return View();
    }

    [HttpPost]
    public IActionResult FillClientCompanyProps(Company clientCompany)
    {
        if (!ModelState.IsValid)
        {
            return View(clientCompany);
        }

        _wizardService.SaveClientCompanyProps(clientCompany);

        return RedirectToAction("FillExecutorCompanyProps");
    }
    
    public IActionResult FillExecutorCompanyProps()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult FillExecutorCompanyProps(Company executorCompany)
    {
        if (!ModelState.IsValid)
        {
            return View(executorCompany);
        }

        _wizardService.SaveExecutorCompanyProps(executorCompany);

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
        return RedirectToAction("UploadDocuments");
    }

    public IActionResult UploadDocuments()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UploadDocumentsAsync(List<IFormFile> files)
    {
        var documents = new List<DocumentData>();
        foreach (var file in files)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var d = memoryStream.ToArray();
            

            var document = new Document
            {
                Name = file.FileName,
                //TODO filepath needs to be added in the end
                ContentType = file.ContentType,
                FileSize = file.Length
            };
            
            documents.Add(new DocumentData
            {
                Document = document,
                Data = d    
            });
        }
        
        _wizardService.SaveDocuments(documents);
        return View();
    }

    public JsonResult SearchEmployeesByQuery(string query)
    {
        return Json(_wizardService.GetEmployeesByQuery(query));
    }
}