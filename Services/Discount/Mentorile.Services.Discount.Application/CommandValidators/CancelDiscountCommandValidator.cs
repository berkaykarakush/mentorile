using FluentValidation;
using Mentorile.Services.Discount.Application.Commands;

namespace Mentorile.Services.Discount.Application.CommandValidators;
public class CancelDiscountCommandValidator : AbstractValidator<CancelDiscountCommand>
{
    public CancelDiscountCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Discount code cannot be empty.");
    }
}