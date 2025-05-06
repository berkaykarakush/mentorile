using FluentValidation;
using Mentorile.Services.Discount.Application.Commands;

namespace Mentorile.Services.Discount.Application.CommandValidators;
public class DeleteDiscountCommandValidator : AbstractValidator<DeleteDiscountCommand>
{
    public DeleteDiscountCommandValidator()
    {
        RuleFor(x => x.DiscountId)
            .NotEmpty().WithMessage("Discount Id cannot be empty.");
    }
}