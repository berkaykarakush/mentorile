using FluentValidation;
using Mentorile.Services.User.Application.Commands;

namespace Mentorile.Services.User.Application.CommandValidators;
public class SoftDeleteUserCommandValidator : AbstractValidator<SoftDeleteUserCommand>
{
    public SoftDeleteUserCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User Id is required");
    }
}