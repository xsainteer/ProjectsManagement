using API.DTOs;
using AutoMapper;
using Business.Services;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CompaniesController : GenericController<Company>
{
    private readonly IMapper _mapper;


    public CompaniesController(IGenericService<Company> service, ILogger<GenericController<Company>> logger, IMapper mapper) : base(service, logger)
    {
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var company = _mapper.Map<Company>(dto);
            await _service.AddAsync(company);
            return Ok(new { id = company.Id });
        }
        catch (Exception e)
        {
            _logger.LogError("Error while creating company: {ErrorMessage}", e.Message);
            return StatusCode(500, new {message = "An error occurred while creating the company"});
        }
    }
}