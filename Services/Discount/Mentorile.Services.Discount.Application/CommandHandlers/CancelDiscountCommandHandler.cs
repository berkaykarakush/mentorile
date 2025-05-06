using MediatR;
using Mentorile.Services.Discount.Application.Commands;
using Mentorile.Services.Discount.Domain.Interfaces;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Discount.Application.CommandHandlers;
public class CancelDiscountCommandHandler : IRequestHandler<CancelDiscountCommand, Result<bool>>
{
    private readonly IDiscountRepository _discountRepository;

    public CancelDiscountCommandHandler(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }

    public async Task<Result<bool>> Handle(CancelDiscountCommand request, CancellationToken cancellationToken)
    {
        var result = await _discountRepository.CancelDiscountAsync(request.Code);
        if(!result.IsSuccess) return Result<bool>.Failure("Failed to cancel discount.");
        return Result<bool>.Success(result.IsSuccess, "Apply discount successfully.");
    }
}