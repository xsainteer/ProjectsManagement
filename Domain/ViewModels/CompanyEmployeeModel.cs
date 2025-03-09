using Domain.Entities;

namespace Domain.ViewModels;

public class CompanyEmployeeModel
{
    public Company Company { get; set; }
    public List<Employee> Employees { get; set; }
}