using Domain.Entities;

namespace Domain.Interfaces;

public interface IEmployeeService : IGenericService<Employee>
{
    Task CreateEmployeesAsync(List<Employee> employees);
}