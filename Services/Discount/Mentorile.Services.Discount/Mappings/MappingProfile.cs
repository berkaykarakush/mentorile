using AutoMapper;
using Mentorile.Services.Discount.DTOs;

namespace Mentorile.Services.Discount.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Models.Discount, DiscountDTO>().ReverseMap();
        CreateMap<Models.Discount, CreateDiscountDTO>().ReverseMap();
        CreateMap<Models.Discount, UpdateDiscountDTO>().ReverseMap();
    }    
}