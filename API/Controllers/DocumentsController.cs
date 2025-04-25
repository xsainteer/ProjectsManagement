using API.DTOs;
using API.Mappers;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class DocumentsController : GenericController<ProjectDocument>
{
    private readonly IDocumentService _documentService;


    public DocumentsController(IGenericService<ProjectDocument> service, ILogger<GenericController<ProjectDocument>> logger, IDocumentService documentService) : base(service, logger)
    {
        _documentService = documentService;
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
            var documentFiles = dtos.Select(DocumentFileMapper.ToDocumentFile).ToList();
            await _documentService.CreateDocumentsAsync(documents, documentFiles);
            
            return Ok(new {ids = documents.Select(x => x.Id)});
        }
        catch (Exception e)
        {
            _logger.LogError("Error creating documents: {Message}", e.Message);
            throw;
        }
    }
}