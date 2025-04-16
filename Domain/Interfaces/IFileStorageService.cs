using Domain.Entities;

namespace Domain.Interfaces;

public interface IFileStorageService
{
    public Task SaveFilesAsync(List<DocumentFile> files);
}