using FluentValidation;
using Mentorile.Services.Basket.Application.Command;

namespace Mentorile.Services.Basket.Application.CommandValidators;
public class AddItemToBasketCommandValidator : AbstractValidator<AddItemToBasketCommand>
{
    public AddItemToBasketCommandValidator()
    {
        RuleFor(x => x.ItemId)
            .NotEmpty().WithMessage("Item id cannot be empty.");


        RuleFor(x => x.ItemName)
            .NotEmpty().WithMessage("Item name cannot be empty.");

        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Item price cannot be empty.")
            .InclusiveBetween(1, 999999999).WithMessage("Item price must be between 1 and 999999999.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Item quantity must be greate then zero.");
    }
}