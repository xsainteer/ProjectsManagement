using AutoFixture;
using Business.Services;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests;

public abstract class GenericServiceTests<T> where T : class, IHasId
{
    private readonly GenericService<T> _service;
    private readonly Mock<IGenericRepository<T>> _repository;
    private readonly Mock<ILogger<GenericService<T>>> _logger;
    private readonly Fixture _fixture;

    protected GenericServiceTests()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.Remove(
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().First());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        _logger = new Mock<ILogger<GenericService<T>>>();
        _repository = new Mock<IGenericRepository<T>>();
        _service = new GenericService<T>(_repository.Object, _logger.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsEntity_WhenEntityExists()
    {
        // Arrange
        var expectedId = Guid.NewGuid();
        var entity = CreateEntity(expectedId);
        
        _repository.Setup(repo => repo.GetByIdAsync(expectedId)).ReturnsAsync(entity);
        
        var result = await _service.GetByIdAsync(expectedId);
        
        Assert.NotNull(result);
        Assert.Equal(expectedId, result.Id);
    }

    public T CreateEntity(Guid id)
    {
        var entity = _fixture.Build<T>()
            .Create();
        
        typeof(T).GetProperty("Id")?.SetValue(entity, id);
        
        return entity;
    }
}

public class ProjectServiceTests : GenericServiceTests<Project> { }