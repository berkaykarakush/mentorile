using AutoMapper;
using Mentorile.Services.Discount.Application.Commands;
using Mentorile.Services.Discount.Application.DTOs;

namespace Mentorile.Services.Discount.Application.Mapping;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.Discount, DiscountDTO>().ReverseMap();
        CreateMap<Domain.Entities.DiscountUser, DiscountUserDTO>().ReverseMap();
        CreateMap<Domain.Entities.Discount, CreateDiscountCommand>().ReverseMap();
        CreateMap<Domain.Entities.Discount, UpdateDiscountCommand>().ReverseMap();
    }    
}