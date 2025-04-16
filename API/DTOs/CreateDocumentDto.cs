namespace API.DTOs;

public class CreateDocumentDto
{
    public Guid ProjectId { get; set; }
    public IFormFile File { get; set; } = null!;
}