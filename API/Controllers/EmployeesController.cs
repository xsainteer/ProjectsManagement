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
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeesByProjectId(
        [FromQuery] Guid projectId, 
        [FromQuery] string query = "",
        [FromQuery] int skip = 0, 
        [FromQuery] int count = 10)
    {
        try
        {
            var employees = await _employeeService
                .GetAllByProjectIdAsync(projectId, skip, count, true, query);
            
            return Ok(employees);
        }
        catch (Exception e)
        {
            _logger.LogError("Error while fetching employees: {ErrorMessage}", e.Message);
            return StatusCode(500, new {message = "An error occurred while fetching employees"});
        }
    }
}