namespace Domain.Entities;

public class ProjectDocument
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string FilePath { get; set; }
    public string ContentType { get; set; }
    public long FileSize { get; set; }
    
    public Guid ProjectId { get; set; }
    public Project Project { get; set; }
}