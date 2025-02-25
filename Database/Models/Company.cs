namespace Database.Models;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Project> Projects { get; set; } 
}