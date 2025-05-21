using AutoMapper;
using Mentorile.Services.Basket.Application.Command;
using Mentorile.Services.Basket.Application.DTOs;
using Mentorile.Services.Basket.Domain.Entities;

namespace Mentorile.Services.Basket.Application.Mapping;
public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<Domain.Entities.Basket, BasketDTO>().ReverseMap();
        CreateMap<BasketItem, BasketItemDTO>().ReverseMap();
        CreateMap<AddItemToBasketCommand, BasketItem>().ReverseMap();
    }
}