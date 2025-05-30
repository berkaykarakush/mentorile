using FluentValidation;
using Mentorile.IdentityServer.Commands;

namespace Mentorile.IdentityServer.CommandValidators;
public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId field is required.");

        RuleFor(x => x.CurrentPassword)
            .NotEmpty().WithMessage("Current password field is required.");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("New password field is required.");

        RuleFor(x => x.ReNewPassword)
            .NotEmpty().WithMessage("New password again field is required.")
            .Equal(x => x.NewPassword).WithMessage("Passwords do not match.");
    }
}