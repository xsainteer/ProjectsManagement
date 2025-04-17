using API.DTOs;
using API.Mappers;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class DocumentsController : ControllerBase
{
    private readonly IDocumentService _documentService;
    private readonly ILogger<DocumentsController> _logger;
    private readonly IFileStorageService _fileStorageService;

    public DocumentsController(IDocumentService documentService, ILogger<DocumentsController> logger, IFileStorageService fileStorageService)
    {
        _documentService = documentService;
        _logger = logger;
        _fileStorageService = fileStorageService;
    }

    [HttpPost("bulk")]
    public async Task<IActionResult> CreateDocuments([FromForm] List<CreateDocumentDto> dtos)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var documents = dtos.Select(ProjectDocumentMapper.ToProjectDocument).ToList();
            await _documentService.CreateDocumentsAsync(documents);
            var documentFiles = dtos.Select(DocumentFileMapper.ToDocumentFile).ToList();
            await _fileStorageService.SaveFilesAsync(documentFiles);
            
            return Ok(new {ids = documents.Select(x => x.Id)});
        }
        catch (Exception e)
        {
            _logger.LogError("Error creating documents: {Message}", e.Message);
            throw;
        }
    }
}