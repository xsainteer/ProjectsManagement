using Domain.Entities;

namespace Domain.Interfaces;

public interface IEmployeeService : IGenericService<Employee>
{
    Task CreateEmployeesAsync(List<Employee> employees);

    Task<List<Employee>> GetAllByProjectIdAsync(
        Guid projectId, int skip = 0, int count = 10, bool asNoTracking = false, string query = "");
}