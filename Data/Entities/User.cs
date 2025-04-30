using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Data.Entities;

public class User : IdentityUser<Guid>
{
    public Employee Employee { get; set; } = null!;
    public Guid EmployeeId { get; set; }

    
    //for api
    public User() {}
    
    //making a user for a specific employee
    public User(Employee employee)
    {
        Employee = employee;
        EmployeeId = employee.Id;
        Id = employee.Id;
    }
}