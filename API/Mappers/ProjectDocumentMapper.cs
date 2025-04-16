using API.DTOs;
using Domain.Entities;

namespace API.Mappers;

public static class ProjectDocumentMapper
{
    public static ProjectDocument ToProjectDocument(this CreateDocumentDto dto)
    {
        var document = new ProjectDocument
        {
            Name = dto.File.FileName,
            FilePath = $"/uploads/{dto.File.FileName}",
            ContentType = dto.File.ContentType,
            FileSize = dto.File.Length,
            ProjectId = dto.ProjectId
        };
        return document;
    }
}