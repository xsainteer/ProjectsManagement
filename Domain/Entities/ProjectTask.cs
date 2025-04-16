using Domain.Enums;

namespace Domain.Entities;

public class ProjectTask
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    
    public Guid? AuthorId { get; set; }
    public Employee Author { get; set; } = null!;

    public Guid? ExecutorId { get; set; }
    public Employee Executor { get; set; } = null!;

    public Guid? ProjectId { get; set; }
    public Project Project { get; set; } = null!;

    public string Comment { get; set; } = null!;
    public Status Status { get; set; }
    public Priority Priority { get; set; }
}