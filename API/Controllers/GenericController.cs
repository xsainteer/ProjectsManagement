using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class GenericController<T> : ControllerBase where T : IHasId
{
    protected readonly ILogger<GenericController<T>> _logger;
    protected readonly IGenericService<T> _service;

    public GenericController(IGenericService<T> service, ILogger<GenericController<T>> logger)
    {
        _service = service;
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int skip = 0, 
        [FromQuery] int count = 10, 
        [FromQuery] string query = "")
    {
        try
        {
            var result = await _service.GetAllAsync(skip, count, true, query);
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError("Error while fetching {Type} entities: {Message}",typeof(T).Name, e.Message);
            return StatusCode(500, new {message = "An error occurred while fetching data"});
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError("Error while fetching {Type} entities: {Message}",typeof(T).Name, e.Message);
            throw;
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById(Guid id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError("Error while deleting {Type} entities: {Message}",typeof(T).Name, e.Message);
            throw;
        }
    }
}