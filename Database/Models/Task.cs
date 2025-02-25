namespace Database.Models;

public class Task
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public int AuthorId { get; set; }
    public Employee Author { get; set; }
    
    public int ExecutorId { get; set; }
    public Employee Executor { get; set; }
    
    public TaskStatus Status { get; set; }
    public string Comment { get; set; }
    public TaskPriority Priority { get; set; }
}

public enum TaskStatus
{
    Todo,
    InProgress,
    Done
}

public enum TaskPriority
{
    Low = 1,
    Medium = 2,
    High = 3
}