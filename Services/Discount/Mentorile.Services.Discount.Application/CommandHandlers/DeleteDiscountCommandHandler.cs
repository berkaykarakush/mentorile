using MediatR;
using Mentorile.Services.Discount.Application.Commands;
using Mentorile.Services.Discount.Domain.Interfaces;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Discount.Application.CommandHandlers;
public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand, Result<bool>>
{
    private readonly IDiscountRepository _discountRepository;

    public DeleteDiscountCommandHandler(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }

    public async Task<Result<bool>> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
    {
        var result = await _discountRepository.DeleteAsync(request.DiscountId);
        if(!result.IsSuccess) return Result<bool>.Failure("Failed to discount delete.");
        return Result<bool>.Success(result.IsSuccess, "Discount deleted successfully.");
    }
}