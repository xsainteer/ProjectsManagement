using Business.Configuration;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Options;

namespace Business.Services;

public class FileStorageService : IFileStorageService
{
    private readonly FileStorageSettings _settings;

    public FileStorageService(IOptions<FileStorageSettings> options)
    {
        _settings = options.Value;
    }


    public async Task SaveFilesAsync(List<DocumentFile> files)
    {
        foreach (var file in files)
        {
            var filePath = Path.Combine(_settings.FilePath, file.FileName);
            
            await File.WriteAllBytesAsync(filePath, file.Content);
        }   
    }
}