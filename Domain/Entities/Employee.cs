using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Employee
{
    public int Guid { get; set; }
    
    [Required(ErrorMessage = "Employee name is required")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Employee speciality is required")]
    public string Speciality { get; set; }
    public List<ProjectTask>? TasksAsAuthor { get; set; }
    public List<ProjectTask>? TasksAsExecutor { get; set; }
    
    public List<EmployeeProject> EmployeeProjects { get; set; } = new();
    
    public Guid CompanyId { get; set; }
    public Company Company { get; set; }
}