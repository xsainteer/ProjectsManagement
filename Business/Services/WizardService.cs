using System.Text.Json;
using Data.Repositories;
using Domain.DTOs;
using Domain.Entities;
using Domain.ViewModels;

namespace Business.Services;

public interface IWizardService
{
    void SaveProjectProps(Project project);
    void SaveClientCompanyProps(Company company);
    void SaveExecutorCompanyProps(Company company);
    List<Employee> GetEmployeesByQuery(string query);
    void SaveSupervisorAndExecutors(string jsonSupervisor, string jsonExecutors);
    void SaveDocuments(List<DocumentData> documents);
}

public class WizardService : IWizardService
{
    private readonly IGenericRepository<Project> _projectRepository;
    private readonly ISessionService _sessionService;
    private WizardViewModel WizardViewModel { get; set; }
    
    public WizardService(IGenericRepository<Project> projectRepository, ISessionService sessionService)
    {
        _projectRepository = projectRepository;
        _sessionService = sessionService;
    }

    //stores project properties' values in session
    public void SaveProjectProps(Project project)
    {
        _sessionService.SetSessionData("ProjectProps", project);
    }

    public void SaveClientCompanyProps(Company company)
    {
        _sessionService.SetSessionData("ClientCompanyProps", company);
    }
    
    public void SaveExecutorCompanyProps(Company company)
    {
        _sessionService.SetSessionData("ExecutorCompanyProps", company);
    }

    public List<Employee> GetEmployeesByQuery(string query)
    {
        var employees = _sessionService.GetSessionData<Company>("CompanyProps")
            .Employees
            .Where(e => e.Name.Contains(query))
            .Take(10)
            .ToList();

        return employees;
    }

    public void SaveSupervisorAndExecutors(string jsonSupervisor, string jsonExecutors)
    {
        var supervisor = JsonSerializer.Deserialize<Employee>(jsonSupervisor);
        var executors = JsonSerializer.Deserialize<List<Employee>>(jsonExecutors);

        _sessionService.SetSessionData("SupervisorProps", supervisor);
        _sessionService.SetSessionData("ExecutorsProps", executors);
    }

    public void SaveDocuments(List<DocumentData> documents)
    {
        _sessionService.SetSessionData("DocumentsProps", documents);
    }

    public void UploadAllData()
    {
        var project = _sessionService.GetSessionData<Project>("ProjectProps");
        var clientCompany = _sessionService.GetSessionData<Company>("ClientCompanyProps");
        var executorCompany = _sessionService.GetSessionData<Company>("ExecutorCompanyProps");
        var supervisor = _sessionService.GetSessionData<Employee>("SupervisorProps");
        var executors = _sessionService.GetSessionData<List<Employee>>("ExecutorsProps");
        //TODO save allat in db
    }
}