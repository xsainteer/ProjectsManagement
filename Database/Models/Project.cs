namespace Database.Models;

public class Project
{
    public int Id { get; set; }
    public string ProjectName { get; set; } = null!;
    
    public int ClientCompanyId { get; set; }
    public Company ClientCompany { get; set; } = null!;
    
    public int ExecutorCompanyId { get; set; }
    public Company ExecutorCompany { get; set; } = null!;
    
    
    public List<Employee> Employees { get; set; } = new();
    
    public int SupervisorId { get; set; }
    public Employee Supervisor { get; set; } = null!;
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public List<ProjectTask> Tasks { get; set; } = new();
}