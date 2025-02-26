namespace Database.Models;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Speciality { get; set; }
    public List<ProjectTask> TasksAsAuthor { get; set; }
    public List<ProjectTask> TasksAsExecutor { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; }
    
    public int CompanyId { get; set; }
    public Company Company { get; set; }
}