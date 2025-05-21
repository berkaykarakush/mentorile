using MediatR;
using Mentorile.Services.Basket.Application.Command;
using Mentorile.Services.Basket.Domain.Interfaces;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Basket.Application.CommandHandlers;

public class RemoveItemFromBasketCommandHandler : IRequestHandler<RemoveItemFromBasketCommand, Result<bool>>
{
    private readonly IBasketRepository _basketRepository;

    public RemoveItemFromBasketCommandHandler(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    public async Task<Result<bool>> Handle(RemoveItemFromBasketCommand request, CancellationToken cancellationToken)
    {
        var result = await _basketRepository.RemoveItemFromBasketAsync(request.ItemId);
        if (!result.IsSuccess) return Result<bool>.Failure("Item could not be removed from basket.");
        return Result<bool>.Success(result.IsSuccess, "Item successfully removed from basket.");
    }
}