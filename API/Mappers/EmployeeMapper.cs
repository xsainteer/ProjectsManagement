using API.DTOs;
using Domain.Entities;

namespace API.Mappers;

public static class EmployeeMapper
{
    public static Employee ToEmployee(this CreateEmployeeDto dto)
    {
        var employee = new Employee
        {
            Name = dto.Name,
            Speciality = dto.Speciality,
            CompanyId = dto.CompanyId
        };
        if (dto.IsPerformer)
        {
            employee.EmployeeProjects.Add(new EmployeeProject()
            {
                ProjectId = dto.ProjectId,
                Employee = employee
            });
        }

        if (dto.IsManager)
        {
            employee.SupervisedProjectId = dto.ProjectId;
        }
        return employee;
    }
}