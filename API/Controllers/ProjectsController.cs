using API.DTOs;
using AutoMapper;
using Business.Services;
using Data;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ProjectsController : GenericController<Project>
{
    
    private readonly IMapper _mapper;


    public ProjectsController(IGenericService<Project> service, ILogger<GenericController<Project>> logger, IMapper mapper) : base(service, logger)
    {
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto dto)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        try
        {
            var project = _mapper.Map<Project>(dto);
            
            await _service.AddAsync(project);

            return Ok(new {id = project.Id});
        }
        catch (Exception e)
        {
            _logger.LogError("Error while creating project: {ErrorMessage}", e.Message);
            return StatusCode(500, new {message = "An error occurred while creating the project"});
        }
    }

    // [HttpGet]
    // public async Task<IActionResult> GetAllProjects([FromQuery] string? query, int skip = 0, int count = 10)
    // {
    //     try
    //     {
    //         var projects = await _service.GetAllAsync(skip, count, true, query);
    //         return Ok(projects);
    //     }
    //     catch (Exception e)
    //     {
    //         _logger.LogError("Error while getting all projects: {ErrorMessage}", e.Message);
    //         throw;
    //     }
    // }
}