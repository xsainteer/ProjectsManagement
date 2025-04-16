using API.DTOs;
using AutoMapper;
using Domain.Entities;

namespace API.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateProjectDto, Project>();
        CreateMap<CreateCompanyDto, Company>();
    }
}