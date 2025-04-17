using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Business.Services;

public class EmployeeService : GenericService<Employee>, IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    public EmployeeService(IGenericRepository<Employee> repository, ILogger<GenericService<Employee>> logger, IEmployeeRepository employeeRepository) : base(repository, logger)
    {
        _employeeRepository = employeeRepository;
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

    public async Task<List<Employee>> GetAllByProjectIdAsync(
        Guid projectId, int skip = 0, int count = 10, bool asNoTracking = false, string query = "")
    {
        var employees = await _employeeRepository.GetAllByProjectIdAsync(projectId, skip, count, asNoTracking);

        return employees;
    }
}