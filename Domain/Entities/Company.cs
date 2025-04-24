using Domain.Interfaces;

namespace Domain.Entities;

public class Company : INameable, IHasId
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public List<Project>? ProjectsAsClient { get; set; } = [];
    public List<Project>? ProjectsAsExecutor { get; set; } = [];
    public List<Employee>? Employees { get; set; } = []; 
}