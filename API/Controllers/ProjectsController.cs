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
public class ProjectsController : ControllerBase
{
    private readonly IGenericService<Project> _projectService;
    private readonly ILogger<ProjectsController> _logger;
    private readonly IMapper _mapper;
    
    public ProjectsController(IGenericService<Project> projectService, ILogger<ProjectsController> logger, IMapper mapper)
    {
        _projectService = projectService;
        _logger = logger;
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
            
            await _projectService.AddAsync(project);

            return Ok(new {id = project.Id});
        }
        catch (Exception e)
        {
            _logger.LogError("Error while creating project: {ErrorMessage}", e.Message);
            return StatusCode(500, new {message = "An error occurred while creating the project"});
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProjects([FromQuery] string? query, int skip = 0, int count = 10)
    {
        try
        {
            var projects = await _projectService.GetAllAsync(skip, count, true, query);
            return Ok(projects);
        }
        catch (Exception e)
        {
            _logger.LogError("Error while getting all projects: {ErrorMessage}", e.Message);
            throw;
        }
    }
}