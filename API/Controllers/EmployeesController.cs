using API.DTOs;
using API.Mappers;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<EmployeesController> _logger;

    public EmployeesController(IEmployeeService employeeService, ILogger<EmployeesController> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }
    
    // there is no reason to use DTO when creating a project
    // because we are not sending any data to the client and every property of the
    // frontend project is already in the project entity
    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] Employee employee)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _employeeService.AddAsync(employee);
        }
        catch (Exception e)
        {
            _logger.LogError("Error while creating employee: {ErrorMessage}", e.Message);
            return StatusCode(500, new {message = "An error occurred while creating the employee"});
        }
        return Ok();
    }
    
    [HttpPost("bulk")]
    public async Task<IActionResult> CreateEmployees([FromBody] List<CreateEmployeeDto> dtos)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var employees = dtos.Select(EmployeeMapper.ToEmployee).ToList();
            
            await _employeeService.CreateEmployeesAsync(employees);
            
            return Ok(new {ids = employees.Select(e => e.Id)});
        }
        catch (Exception e)
        {
            _logger.LogError("Error while creating employees: {ErrorMessage}", e.Message);
            return StatusCode(500, new {message = "An error occurred while creating employees"});
        }
    }
}