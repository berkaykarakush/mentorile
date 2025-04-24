using AutoMapper;
using Mentorile.Services.Study.Application.DTOs;

namespace Mentorile.Services.Study.Application.Mapping;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Core.Study, StudyDTO>().ReverseMap();
    }
}