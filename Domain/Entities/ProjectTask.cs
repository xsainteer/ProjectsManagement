using Domain.Enums;

namespace Domain.Entities;

public class ProjectTask
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public Guid AuthorId { get; set; }
    public Employee Author { get; set; }
    
    public Guid ExecutorId { get; set; }
    public Employee Executor { get; set; }
    
    public Guid ProjectId { get; set; }
    public Project Project { get; set; }
    
    public string Comment { get; set; }
    public Status Status { get; set; }
    public Priority Priority { get; set; }
}