using AutoMapper;
using Mentorile.Services.User.Application.DTOs;
using Mentorile.Services.User.Domain.Entities;

namespace Mentorile.Services.User.Application.Mapping;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserProfile, UserDTO>().ReverseMap();
    }
}