using API.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly IGenericService<ProjectTask> _genericService;
    private readonly IMapper _mapper;
    private readonly ILogger<TasksController> _logger;

    public TasksController(IGenericService<ProjectTask> genericService, IMapper mapper, ILogger<TasksController> logger)
    {
        _genericService = genericService;
        _mapper = mapper;
        _logger = logger;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto createTaskDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var projectTask = _mapper.Map<ProjectTask>(createTaskDto);
            await _genericService.AddAsync(projectTask);
            return Ok(new { id = projectTask.Id });
        }
        catch (Exception e)
        {
            _logger.LogError("Error while creating task: {ErrorMessage}", e.Message);
            throw;
        }
    }
}