using FluentValidation;
using Mentorile.Services.Payment.Application.Queries;

namespace Mentorile.Services.Payment.Application.QueryValidators;
public class GetAllPaymentsByStatusQueryValidator : AbstractValidator<GetAllPaymentsByStatusQuery>
{
    public GetAllPaymentsByStatusQueryValidator()
    {
        RuleFor(x => x.PaymentStatus)
            .IsInEnum().WithMessage("Invalid Payment status value.");

        RuleFor(x => x.PagingParams.PageNumber)
            .GreaterThan(0).WithMessage("Page number must be greather than 0.");
        
        RuleFor(x => x.PagingParams.PageSize)
            .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100.");
    }
}