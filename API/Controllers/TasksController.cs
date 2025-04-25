using API.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TasksController : GenericController<ProjectTask>
{
    private readonly IMapper _mapper;


    public TasksController(IGenericService<ProjectTask> service, ILogger<GenericController<ProjectTask>> logger, IMapper mapper) : base(service, logger)
    {
        _mapper = mapper;
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
            await _service.AddAsync(projectTask);
            return Ok(new { id = projectTask.Id });
        }
        catch (Exception e)
        {
            _logger.LogError("Error while creating task: {ErrorMessage}", e.Message);
            throw;
        }
    }
}