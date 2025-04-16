namespace API.DTOs;

public class CreateEmployeeDto
{
    public string? Name { get; set; }
    public string? Speciality { get; set; }
    public Guid CompanyId { get; set; }
    public bool IsPerformer { get; set; }
    public bool IsManager { get; set; }
    public Guid ProjectId { get; set; }
}