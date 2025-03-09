using Data.Repositories;
using Domain.Entities;
using Domain.ViewModels;

namespace Business.Services;

public interface IWizardService
{
    void FillProjectProps(Project project);
    void FillCompanyProps(Company company);
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
    public void FillProjectProps(Project project)
    {
        _sessionService.SetSessionData("ProjectProps", project);
    }

    public void FillCompanyProps(Company company)
    {
        _sessionService.SetSessionData("CompanyProps", company);
    }
}