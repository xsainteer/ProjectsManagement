using System.Text.Json;
using System.Text.Json.Nodes;
using Data.Repositories;
using Domain.Entities;
using Domain.ViewModels;

namespace Business.Services;

public interface IWizardService
{
    void SaveProjectProps(Project project);
    void SaveCompanyProps(Company company);
    List<Employee> GetEmployeesByQuery(string query);
    void SaveSupervisorAndExecutors(string jsonSupervisor, string jsonExecutors);
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

    public void SaveCompanyProps(Company company)
    {
        _sessionService.SetSessionData("CompanyProps", company);
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

        _sessionService.SetSessionData("Supervisor", supervisor);
        _sessionService.SetSessionData("Executors", executors);
    }
}