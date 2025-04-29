using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Data.Entities;

public class User : IdentityUser
{
    public Employee Employee { get; set; } = null!;
    public Guid EmployeeId { get; set; }
}