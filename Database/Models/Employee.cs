namespace Database.Models;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Speciality { get; set; }
    public List<Task> Tasks { get; set; }
    
    public int ProjectId { get; set; }
    public Project Project { get; set; }
}