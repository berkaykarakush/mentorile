using AutoMapper;
using Mentorile.Services.Note.Application.DTOs;

namespace Mentorile.Services.Note.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.Note, NoteDTO>().ReverseMap();        
    }
}