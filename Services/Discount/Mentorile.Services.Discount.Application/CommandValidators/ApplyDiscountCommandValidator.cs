using FluentValidation;
using Mentorile.Services.Discount.Application.Commands;

namespace Mentorile.Services.Discount.Application.CommandValidators;
public class ApplyDiscountCommandValidator : AbstractValidator<ApplyDiscountCommand>
{
    public ApplyDiscountCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Discount code cannot be empty.");

        RuleFor(x => x.TotalPrice)
            .GreaterThan(0).WithMessage("Total price must be greate then zero.");
    }    
}