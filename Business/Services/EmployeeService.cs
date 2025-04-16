using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Business.Services;

public class EmployeeService : GenericService<Employee>, IEmployeeService
{
    public EmployeeService(IGenericRepository<Employee> repository, ILogger<GenericService<Employee>> logger) : base(repository, logger)
    {
    }

    public async Task CreateEmployeesAsync(List<Employee> employees)
    {
        try
        {
            await _repository.AddRangeAsync(employees);
            await _repository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("Error creating employees: {Message}", e.Message);
            throw;
        }
        
        _logger.LogInformation("Employees created successfully");
        await _repository.SaveChangesAsync();
    }
}