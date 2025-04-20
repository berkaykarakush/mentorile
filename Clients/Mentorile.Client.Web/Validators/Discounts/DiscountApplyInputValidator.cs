using FluentValidation;
using Mentorile.Client.Web.ViewModels.Discounts;

namespace Mentorile.Client.Web.Validators.Discounts;
public class DiscountApplyInputValidator : AbstractValidator<DiscountApplyInput>
{
    public DiscountApplyInputValidator()
    {
        RuleFor(x => x.Code)
        .NotEmpty()
        .WithMessage("{PropertyName} alanı boş olamaz.")
        .WithName("İndirim kodu");
    }
}