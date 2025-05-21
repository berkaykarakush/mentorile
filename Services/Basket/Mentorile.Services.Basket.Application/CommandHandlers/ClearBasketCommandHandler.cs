using MediatR;
using Mentorile.Services.Basket.Application.Command;
using Mentorile.Services.Basket.Domain.Interfaces;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Basket.Application.CommandHandlers;

public class ClearBasketCommandHandler : IRequestHandler<ClearBasketCommand, Result<bool>>
{
    private readonly IBasketRepository _basketRepository;

    public ClearBasketCommandHandler(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    public async Task<Result<bool>> Handle(ClearBasketCommand request, CancellationToken cancellationToken)
    {
        var result = await _basketRepository.ClearBasketAsync();
        if (!result.IsSuccess) return Result<bool>.Failure("Basket could not be cleaned.");
        return Result<bool>.Success(result.IsSuccess, "Basket successfully cleaned.");
    }
}