using System.ComponentModel.DataAnnotations;
using Domain.Interfaces;

namespace Domain.Entities;

public class Employee : INameable, IHasId
{
    public Guid Id { get; init; }
    
    public string? Name { get; set; }
    
    public string? Speciality { get; set; }

    public List<ProjectTask>? TasksAsAuthor { get; set; } = [];
    public List<ProjectTask>? TasksAsExecutor { get; set; } = [];
    
    public List<EmployeeProject> EmployeeProjects { get; set; } = new();
    
    public Guid? SupervisedProjectId { get; set; }
    public Project SupervisedProject { get; set; } = null!;
    
    public Guid? CompanyId { get; set; }
    public Company Company { get; set; } = null!;
}