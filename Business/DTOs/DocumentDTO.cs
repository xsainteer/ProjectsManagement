namespace Business.DTOs;

public class DocumentDto
{
    public string Name { get; set; }
    public string FilePath { get; set; }
    public string ContentType { get; set; }
    public long FileSize { get; set; }
    
    public Guid ProjectId { get; set; }
    
    public Stream Stream { get; set; }
}