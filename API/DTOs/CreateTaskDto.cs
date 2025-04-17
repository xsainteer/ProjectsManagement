using Domain.Enums;

namespace API.DTOs;

public class CreateTaskDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Comment { get; set; }
    public Guid? AuthorId { get; set; }
    public Guid? ExecutorId { get; set; }
    public Guid? ProjectId { get; set; }
    public Status? Status { get; set; }
    public Priority? Priority { get; set; }
}