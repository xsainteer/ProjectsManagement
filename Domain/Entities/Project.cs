using System.ComponentModel.DataAnnotations;
using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Entities;

public class Project : INameable, IHasId
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public Guid? ClientCompanyId { get; set; }
    public Company ClientCompany { get; set; } = null!;
    
    public Guid? ContractorCompanyId { get; set; }
    public Company ContractorCompany { get; set; } = null!;
    
    public Guid? SupervisorId { get; set; }
    public Employee? Supervisor { get; set; } = null!;
    
    public List<EmployeeProject> EmployeeProjects { get; set; } = new();
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public List<ProjectTask> Tasks { get; set; } = new();
    public List<ProjectDocument> Documents { get; set; } = new();
    
    public Priority Priority { get; set; }
}

