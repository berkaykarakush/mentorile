using MediatR;
using Mentorile.Services.Basket.Application.Command;
using Mentorile.Services.Basket.Domain.Interfaces;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Basket.Application.CommandHandlers;

public class CancelDiscountCommandHandler : IRequestHandler<CancelDiscountCommand, Result<bool>>
{
    private readonly IBasketRepository _basketRepository;

    public CancelDiscountCommandHandler(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    public async Task<Result<bool>> Handle(CancelDiscountCommand request, CancellationToken cancellationToken)
    {
        var result = await _basketRepository.CancelDiscountAsync(request.DiscountCode);
        if (!result.IsSuccess) return Result<bool>.Failure("Discount could not be canceled.");
        return Result<bool>.Success(result.IsSuccess, "Discount successfully canceled.");
    }
}