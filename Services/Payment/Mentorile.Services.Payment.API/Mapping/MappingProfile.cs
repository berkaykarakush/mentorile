using AutoMapper;
using Mentorile.Services.Payment.API.DTOs;

namespace Mentorile.Services.Payment.API.Mapping;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Models.Payment, PaymentDTO>().ReverseMap();
    }    
}