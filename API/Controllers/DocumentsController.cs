using Business.DTOs;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class DocumentsController : ControllerBase
{
    private readonly DocumentService _documentService;
    private readonly ILogger<DocumentsController> _logger;

    public DocumentsController(DocumentService documentService, ILogger<DocumentsController> logger)
    {
        _documentService = documentService;
        _logger = logger;
    }

    [HttpPost("bulk")]
    public async Task<IActionResult> CreateDocuments([FromForm] List<IFormFile> files, [FromForm] Guid projectId)
    {
        var documentDtos = files.Select(file => new DocumentDto
        {
            Name = file.FileName,
            FilePath = Path.Combine("uploads", file.FileName),
            ContentType = file.ContentType,
            FileSize = file.Length,
            ProjectId = projectId,
            Stream = file.OpenReadStream()
        }).ToList();

        try
        {
            await _documentService.CreateDocuments(documentDtos);
        }
        catch (Exception e)
        {
            _logger.LogError("Error while creating documents: {ErrorMessage}", e.Message);
            return StatusCode(500, new {message = "An error occured while creating documents"});
        }
        
        return Ok();
    }
}