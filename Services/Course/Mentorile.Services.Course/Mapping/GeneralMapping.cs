using AutoMapper;
using Mentorile.Services.Course.DTOs.Course;

namespace Mentorile.Services.Course.Mapping;
public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<CourseDTO, Models.Course>().ReverseMap();
        CreateMap<CreateCourseDTO, Models.Course>().ReverseMap();
        CreateMap<UpdateCourseDTO, Models.Course>().ReverseMap();            
    }
}