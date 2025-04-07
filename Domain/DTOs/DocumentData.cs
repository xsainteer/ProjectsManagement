using System.Reflection.Metadata;

namespace Domain.DTOs;

public class DocumentData
{
    public Domain.Entities.Document Document { get; set; }
    public byte[] Data { get; set; } = [];
}