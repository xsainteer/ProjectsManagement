using Data.Repositories;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Business.DTOs;


namespace Business.Services;

public class DocumentService : GenericService<ProjectDocument>
{
    public DocumentService(IGenericRepository<ProjectDocument> repository, ILogger<GenericService<ProjectDocument>> logger) : base(repository, logger)
    {
    }

    public async Task CreateDocuments(List<DocumentDto> documentDtos)
    {
        foreach (var documentDto in documentDtos)
        {
            await using var fileStream = new FileStream(documentDto.FilePath, FileMode.Create);
            await documentDto.Stream.CopyToAsync(fileStream);
        }
        
        var documents = documentDtos.Select(dto => new ProjectDocument
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            FilePath = dto.FilePath,
            ContentType = dto.ContentType,
            FileSize = dto.FileSize,
            ProjectId = dto.ProjectId
        }).ToList();

        foreach (var document in documents)
        {
            try
            {
                await _repository.AddAsync(document);
            }
            catch (Exception e)
            {
                _logger.LogError("Error while creating document: {ErrorMessage}", e.Message);
                throw;
            }
            _logger.LogInformation("Document {DocumentId} has been created", document.Id);
            await _repository.SaveChangesAsync();
        }
    }
}