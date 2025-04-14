using Business.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CompaniesController : ControllerBase
{
    private readonly IGenericService<Company> _companyService;
    private readonly ILogger<CompaniesController> _logger;

    public CompaniesController(GenericService<Company> companyService, ILogger<CompaniesController> logger)
    {
        _companyService = companyService;
        _logger = logger;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCompany([FromBody] Company company)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _companyService.AddAsync(company);
        }
        catch (Exception e)
        {
            _logger.LogError("Error while creating company: {ErrorMessage}", e.Message);
            return StatusCode(500, new {message = "An error occurred while creating the company"});
        }
        return Ok();
    }
}