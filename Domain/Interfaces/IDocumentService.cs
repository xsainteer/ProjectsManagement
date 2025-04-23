using Domain.Entities;

namespace Domain.Interfaces;

public interface IDocumentService : IGenericService<ProjectDocument>
{
    Task CreateDocumentsAsync(List<ProjectDocument> documents, List<DocumentFile> documentFiles);
}