using Domain.Entities;
using Microsoft.Extensions.Logging;
using Domain.Interfaces;


namespace Business.Services;

public class DocumentService : GenericService<ProjectDocument>, IDocumentService
{
    private readonly IFileStorageService _fileStorageService;
    public DocumentService(IGenericRepository<ProjectDocument> repository, ILogger<GenericService<ProjectDocument>> logger, IFileStorageService fileStorageService)
        : base(repository, logger)
    {
        _fileStorageService = fileStorageService;
    }

    public async Task CreateDocumentsAsync(List<ProjectDocument> documents, List<DocumentFile> documentFiles)
    {
        try
        {
            await _repository.AddRangeAsync(documents);

            await _fileStorageService.SaveFilesAsync(documentFiles);
            
            await _repository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("Error creating documents: {Message}", e.Message);
            throw;
        }
    }
}