using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data.Repositories;

public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(AppDbContext context, ILogger<GenericRepository<Employee>> logger) : base(context, logger)
    {
    }

    public async Task<List<Employee>> GetAllByProjectIdAsync(Guid projectId, int skip = 0, int count = 10, bool asNoTracking = false, string query = "")
    {
        try
        {
            var set = _dbSet.Include(e => e.EmployeeProjects);
            
            var queryable = asNoTracking ? set.AsNoTracking() : set;

            var list = queryable
                .Where(e => e.EmployeeProjects.Any(ep => ep.ProjectId == projectId))
                .Select(e => new Employee
                {
                    Id = e.Id,
                    Name = e.Name,
                    Speciality = e.Speciality
                })
                .OrderBy(e => e.Name)
                .Skip(skip)
                .Take(count)
                .ToListAsync();

            return await list;
        }
        catch (Exception e)
        {
            _logger.LogError("error while taking employees: {ErrorMessage}", e.Message);
            throw;
        }
    }
}