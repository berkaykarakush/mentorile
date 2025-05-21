using AutoMapper;
using MediatR;
using Mentorile.Services.Basket.Application.Command;
using Mentorile.Services.Basket.Domain.Entities;
using Mentorile.Services.Basket.Domain.Interfaces;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Basket.Application.CommandHandlers;

public class AddItemToBasketCommandHandler : IRequestHandler<AddItemToBasketCommand, Result<bool>>
{
    private readonly IBasketRepository _basketRepository;
    private readonly IMapper _mapper;
    public AddItemToBasketCommandHandler(IBasketRepository basketRepository, IMapper mapper)
    {
        _basketRepository = basketRepository;
        _mapper = mapper;
    }

    public async Task<Result<bool>> Handle(AddItemToBasketCommand request, CancellationToken cancellationToken)
    {
        var basketItem = _mapper.Map<BasketItem>(request);
        var result = await _basketRepository.AddItemToBasketAsync(basketItem);
        if (!result.IsSuccess) return Result<bool>.Failure("Item could not be added to basket.");
        return Result<bool>.Success(result.IsSuccess, "Item successfully added to basket.");
    }
}