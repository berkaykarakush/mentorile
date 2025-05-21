using FluentValidation;
using Mentorile.IdentityServer.Commands;

namespace Mentorile.IdentityServer.CommandValidators;
public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        // RuleFor(x => x.).NotEmpty().WithMessage("");
    }    
}