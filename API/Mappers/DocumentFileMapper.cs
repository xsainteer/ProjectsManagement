using API.DTOs;
using Domain.Entities;

namespace API.Mappers;

public static class DocumentFileMapper
{
    public static async Task<DocumentFile> ToDocumentFile(this CreateDocumentDto dto)
    {
        using var memoryStream = new MemoryStream();
        await dto.File.CopyToAsync(memoryStream);
        return new DocumentFile
        {
            Content = memoryStream.ToArray(),
            FileName = dto.File.FileName
        };
    }
}