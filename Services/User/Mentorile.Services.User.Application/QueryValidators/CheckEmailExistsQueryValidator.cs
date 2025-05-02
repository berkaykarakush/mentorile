using FluentValidation;
using Mentorile.Services.User.Application.Queries;

namespace Mentorile.Services.User.Application.QueryValidators;
public class CheckEmailExistsQueryValidator : AbstractValidator<CheckEmailExistsQuery>
{
    public CheckEmailExistsQueryValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.");
    }
    
}