using Data.Repositories;
using Domain.Entities;
using Domain.ViewModels;

namespace Business.Services;

public interface IWizardService
{
    
}

public class WizardService : IWizardService
{
    private readonly IGenericRepository<Project> _projectRepository;
    private WizardViewModel WizardViewModel { get; set; }

    public WizardService(IGenericRepository<Project> projectRepository)
    {
        _projectRepository = projectRepository;
    }
}