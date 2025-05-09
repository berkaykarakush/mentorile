using FluentValidation;
using Mentorile.Services.Payment.Application.Queries;

namespace Mentorile.Services.Payment.Application.QueryValidators;
public class GetPaymentByTransactionIdQueryValidator : AbstractValidator<GetPaymentByTransactionIdQuery>
{
    public GetPaymentByTransactionIdQueryValidator()
    {
        RuleFor(x => x.TransactionId)
            .NotEmpty().WithMessage("Transaction id field is required.");
    }
}