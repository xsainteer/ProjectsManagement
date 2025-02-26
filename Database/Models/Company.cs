namespace Database.Models;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Project> ProjectsAsClient { get; set; }
    public List<Project> ProjectsAsExecutor { get; set; } 
    public List<Employee> Employees { get; set; }
    
}