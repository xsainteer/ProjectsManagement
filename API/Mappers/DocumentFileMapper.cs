using API.DTOs;
using Domain.Entities;

namespace API.Mappers;

public static class DocumentFileMapper
{
    public static DocumentFile ToDocumentFile(this CreateDocumentDto dto)
    {
        return new DocumentFile
        {
            Stream = dto.File.OpenReadStream(),
            FileName = dto.File.FileName
        };
    }
}