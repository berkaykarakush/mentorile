using FluentValidation;
using Mentorile.Services.User.Application.Queries;

namespace Mentorile.Services.User.Application.QueryValidators;
public class CheckPhoneNumberExistsQueryValidator : AbstractValidator<CheckPhoneNumberExistsQuery>
{
    public CheckPhoneNumberExistsQueryValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches("^\\+90[0-9]{10}$").WithMessage("A valid phone number is required");
    }
}