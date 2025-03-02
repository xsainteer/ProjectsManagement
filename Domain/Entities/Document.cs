namespace Domain.Entities;

public class Document
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string FilePath { get; set; }
    public string ContentType { get; set; }
    public long FileSize { get; set; }
    
    public int ProjectId { get; set; }
    public Project Project { get; set; }
}