using AutoMapper;
using Mentorile.Services.Course.Application.Commands;
using Mentorile.Services.Course.Application.DTOs;

namespace Mentorile.Services.Course.Application.Mapping;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.Course, CourseDTO>().ReverseMap();
        CreateMap<Domain.Entities.Course, CreateCourseCommand>().ReverseMap();
        CreateMap<Domain.Entities.Course, UpdateCourseCommand>().ReverseMap();
    }
}