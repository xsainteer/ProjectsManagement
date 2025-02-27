namespace Database.Models;

public class ProjectTask
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public int AuthorId { get; set; }
    public Employee Author { get; set; }
    
    public int ExecutorId { get; set; }
    public Employee Executor { get; set; }
    
    public int ProjectId { get; set; }
    public Project Project { get; set; }
    
    public string Comment { get; set; }
    public Status Status { get; set; }
    public Priority Priority { get; set; }
}