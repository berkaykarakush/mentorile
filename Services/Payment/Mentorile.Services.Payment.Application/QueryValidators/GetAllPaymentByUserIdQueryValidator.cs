using System.Data;
using FluentValidation;
using Mentorile.Services.Payment.Application.Queries;

namespace Mentorile.Services.Payment.Application.QueryValidators;
public class GetAllPaymentByUserIdQueryValidator : AbstractValidator<GetAllPaymentByUserIdQuery>
{
    public GetAllPaymentByUserIdQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User id field is required.");

        RuleFor(x => x.PagingParams.PageNumber)
            .GreaterThan(0).WithMessage("Page number must be greather than 0.");
        
        RuleFor(x => x.PagingParams.PageSize)
            .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100.");
    }
}