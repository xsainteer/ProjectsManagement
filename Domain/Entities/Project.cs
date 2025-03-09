using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

public class Project
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Project name is required")]
    public string Name { get; set; } = null!;
    
    public int ClientCompanyId { get; set; }
    public Company ClientCompany { get; set; } = null!;
    
    public int ExecutorCompanyId { get; set; }
    public Company ExecutorCompany { get; set; } = null!;
    
    public int? SupervisorId { get; set; }
    public Employee? Supervisor { get; set; } = null!;
    
    public List<EmployeeProject> EmployeeProjects { get; set; } = new();
    
    
    [Required(ErrorMessage = "Project Start date is required")]
    public DateTime StartDate { get; set; }
    
    [Required(ErrorMessage = "Project End date is required")]
    public DateTime EndDate { get; set; }
    
    public List<ProjectTask> Tasks { get; set; } = new();
    public List<Document> Documents { get; set; } = new();
    
    [Required(ErrorMessage = "Project Priority is required")]
    public Priority Priority { get; set; }
}

