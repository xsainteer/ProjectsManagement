using Domain.Entities;

namespace Domain.ViewModels;

public class WizardViewModel
{
    public Project? Project { get; set; } 
    public Company? ClientCompany { get; set; } 
    public Company? ExecutorCompany { get; set; } 
    public List<Employee>? ClientEmployees { get; set; }
    public List<Employee>? ExecutorEmployees { get; set; }
    public Employee? ExecutorSupervisor { get; set; }
}