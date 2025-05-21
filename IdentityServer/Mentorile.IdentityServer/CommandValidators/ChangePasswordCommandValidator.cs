using FluentValidation;
using Mentorile.IdentityServer.Commands;

namespace Mentorile.IdentityServer.CommandValidators;
public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        // RuleFor(x => x.).NotEmpty().WithMessage("");
    }
}