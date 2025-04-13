using AutoMapper;
using Mentorile.Services.Order.Application.DTOs;

namespace Mentorile.Services.Order.Application.Mapping;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.OrderAggreagate.Order, OrderDTO>().ReverseMap();
        CreateMap<Domain.OrderAggreagate.OrderItem, OrderItemDTO>().ReverseMap();
        CreateMap<Domain.OrderAggreagate.Address, AddressDTO>().ReverseMap();
    }    
}