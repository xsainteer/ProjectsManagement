namespace Domain.Entities;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Speciality { get; set; }
    public List<ProjectTask>? TasksAsAuthor { get; set; }
    public List<ProjectTask>? TasksAsExecutor { get; set; }
    
    public List<EmployeeProject> EmployeeProjects { get; set; } = new();
    
    public int CompanyId { get; set; }
    public Company Company { get; set; }
}