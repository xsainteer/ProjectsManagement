using Data.Repositories;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Business.Services;

public class EmployeeService : GenericService<Employee>
{
    public EmployeeService(IGenericRepository<Employee> repository, ILogger<GenericService<Employee>> logger) : base(repository, logger)
    {
    }

    public async Task CreateEmployeesAsync(List<Employee> employees)
    {
        foreach (var employee in employees)
        {
            try
            {
                await _repository.AddAsync(employee);
            }
            catch (Exception e)
            {
                _logger.LogError("Error while creating employee: {ErrorMessage}", e.Message);
                throw;
            }
        }
        _logger.LogInformation("Employees created successfully");
        await _repository.SaveChangesAsync();
    }
}