using System.Security.Cryptography.X509Certificates;
using FluentValidation;
using Mentorile.Services.Discount.Application.Commands;

namespace Mentorile.Services.Discount.Application.CommandValidators;
public class UpdateDiscountCommandValidator : AbstractValidator<UpdateDiscountCommand>
{
    public UpdateDiscountCommandValidator()
    {
        RuleFor(x => x.Discount)
            .NotEmpty().WithMessage("Discount information is required.");

        RuleFor(x => x.Discount.Id)
            .NotEmpty().WithMessage("Discount Id cannot be empty.");

        RuleFor(x => x.Discount.Rate)
            .InclusiveBetween(1, 100).WithMessage("Discount rate must be between 1 and 100.");

        RuleFor(x => x.Discount.Code)
            .NotEmpty().WithMessage("Discount code cannot be empty. ")
            .MaximumLength(50).WithMessage("Discount code must be at most 50 characters.");

        RuleFor(x => x.Discount.ExpirationDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("Expiration date must be in the future.");
        
        RuleForEach(x => x.Discount.DiscountUsers)
            .ChildRules(user => 
            {
                user.RuleFor(u => u.UserId)
                    .NotEmpty().WithMessage("User Id cannot be empty.");
            });
    }
}