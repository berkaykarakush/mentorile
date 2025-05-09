using FluentValidation;
using Mentorile.Services.Payment.Application.Queries;

namespace Mentorile.Services.Payment.Application.QueryValidators;
public class GetPaymentByOrderIdQueryValidator : AbstractValidator<GetPaymentByOrderIdQuery>
{
    public GetPaymentByOrderIdQueryValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("Order id field is required.");
    }
}