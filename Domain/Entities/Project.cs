using Domain.Enums;

namespace Domain.Entities;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    
    public int ClientCompanyId { get; set; }
    public Company ClientCompany { get; set; } = null!;
    
    public int ExecutorCompanyId { get; set; }
    public Company ExecutorCompany { get; set; } = null!;
    
    public int? SupervisorId { get; set; }
    public Employee? Supervisor { get; set; } = null!;
    
    public List<EmployeeProject> EmployeeProjects { get; set; } = new();
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public List<ProjectTask> Tasks { get; set; } = new();
    public Priority Priority { get; set; }
}

