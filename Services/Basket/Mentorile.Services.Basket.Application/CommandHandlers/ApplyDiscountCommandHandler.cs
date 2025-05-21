using MediatR;
using Mentorile.Services.Basket.Application.Command;
using Mentorile.Services.Basket.Domain.Interfaces;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Basket.Application.CommandHandlers;

public class ApplyDiscountCommandHandler : IRequestHandler<ApplyDiscountCommand, Result<bool>>
{
    private readonly IBasketRepository _basketRepository;

    public ApplyDiscountCommandHandler(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    public async Task<Result<bool>> Handle(ApplyDiscountCommand request, CancellationToken cancellationToken)
    {
        var result = await _basketRepository.ApplyDiscountAsync(request.DiscountCode);
        if (!result.IsSuccess) return Result<bool>.Failure("Discount could not be applied.");
        return Result<bool>.Success(result.IsSuccess, "Discount applied successully.");
    }
}