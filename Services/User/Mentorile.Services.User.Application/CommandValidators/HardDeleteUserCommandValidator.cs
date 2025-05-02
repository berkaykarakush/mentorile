using FluentValidation;
using Mentorile.Services.User.Application.Commands;

namespace Mentorile.Services.User.Application.CommandValidators;
public class HardDeleteUserCommandValidator : AbstractValidator<HardDeleteUserCommand>
{
    public HardDeleteUserCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User Id is required");
    }
}