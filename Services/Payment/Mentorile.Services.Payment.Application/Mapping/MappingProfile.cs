using AutoMapper;
using Mentorile.Services.Payment.Application.Commands;
using Mentorile.Services.Payment.Application.DTOs;

namespace Mentorile.Services.Payment.Application.Mapping;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.Payment, PaymentDTO>().ReverseMap();
        CreateMap<Domain.Entities.Payment, CreatePaymentCommand>().ReverseMap();
        CreateMap<Domain.Entities.Payment, UpdatePaymentCommand>().ReverseMap();
        CreateMap<Domain.Entities.Payment, ReceivePaymentCommand>().ReverseMap();
    }    
}