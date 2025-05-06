using MediatR;
using Mentorile.Services.Discount.Application.Commands;
using Mentorile.Services.Discount.Domain.Interfaces;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Discount.Application.CommandHandlers;
public class ApplyDiscountCommandHandler : IRequestHandler<ApplyDiscountCommand, Result<decimal>>
{
    private readonly IDiscountRepository _discountRepository;

    public ApplyDiscountCommandHandler(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }

    public async Task<Result<decimal>> Handle(ApplyDiscountCommand request, CancellationToken cancellationToken)
    {
        var result = await _discountRepository.ApplyDiscountAsync(request.Code, request.TotalPrice);
        if(!result.IsSuccess) Result<decimal>.Failure("Failed to apply discount.");
        return Result<decimal>.Success(result.Data, "Apply discount successfully.");
    }
}