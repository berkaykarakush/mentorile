using AutoMapper;
using MediatR;
using Mentorile.Services.Basket.Application.DTOs;
using Mentorile.Services.Basket.Application.Queries;
using Mentorile.Services.Basket.Domain.Interfaces;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Basket.Application.QueryHandlers;

public class GetBasketQueryHandler : IRequestHandler<GetBasketQuery, Result<BasketDTO>>
{
    private readonly IBasketRepository _basketRepository;
    private readonly IMapper _mapper;

    public GetBasketQueryHandler(IBasketRepository basketRepository, IMapper mapper)
    {
        _basketRepository = basketRepository;
        _mapper = mapper;
    }

    public async Task<Result<BasketDTO>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        var result = await _basketRepository.GetBasketAsync();
        if (result == null) return Result<BasketDTO>.Failure("Basket not found.");

        var dto = _mapper.Map<BasketDTO>(result.Data);
        return Result<BasketDTO>.Success(dto, "Basket successfully found.");
    }
}