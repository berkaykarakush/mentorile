using System.Data;
using FluentValidation;
using Mentorile.Services.Payment.Application.Queries;

namespace Mentorile.Services.Payment.Application.QueryValidators;
public class GetPaymentByIdQueryValidator : AbstractValidator<GetPaymentByIdQuery>
{
    public GetPaymentByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id field is required.");
    }
}