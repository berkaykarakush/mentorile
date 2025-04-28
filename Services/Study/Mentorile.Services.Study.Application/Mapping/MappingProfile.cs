using AutoMapper;
using Mentorile.Services.Study.Application.Commands;
using Mentorile.Services.Study.Application.DTOs;

namespace Mentorile.Services.Study.Application.Mapping;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.Study, StudyDTO>().ReverseMap();
        CreateMap<Domain.Entities.Study, CreateStudyDTO>().ReverseMap();
        CreateMap<Domain.Entities.Study, UpdateStudyDTO>().ReverseMap();
        CreateMap<Domain.Entities.Study, CreateStudyCommand>().ReverseMap();
        CreateMap<Domain.Entities.Study, UpdateStudyCommand>().ReverseMap();
        CreateMap<Domain.Entities.Study, DeleteStudyCommand>().ReverseMap();
    }
}