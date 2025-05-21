using MediatR;
using Mentorile.Services.Basket.Application.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Basket.Application.Queries;
public class GetBasketQuery : IRequest<Result<BasketDTO>>
{
    
}