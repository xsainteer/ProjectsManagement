namespace API.DTOs;

public class CreateProjectDto
{
    public string? Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Priority { get; set; }
    public Guid ClientCompanyId { get; set; }
    public Guid ContractorCompanyId { get; set; }
}