using API.Mappers;
using Business.Configuration;
using Business.Services;
using Data.Repositories;
using Domain.Interfaces;

namespace API.Extensions;


public static class ServiceCollectionExtensions
{
    public static void AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register services
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IDocumentService, DocumentService>();
        services.AddScoped<IFileStorageService, FileStorageService>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

        // Register AutoMapper
        services.AddAutoMapper(typeof(MappingProfile));

        // Configure FileStorageSettings
        services.Configure<FileStorageSettings>(configuration.GetSection("FileStorageSettings"));
    }
}
