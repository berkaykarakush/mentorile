using AutoMapper;
using Mentorile.Services.PhotoStock.Application.DTOs;

namespace Mentorile.Services.PhotoStock.Application.Mapping;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.Photo, PhotoDTO>().ReverseMap();
    }    
}