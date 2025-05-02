using FluentValidation;

namespace Mentorile.IdentityServer.Handlers;
public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");
        
        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Surname is required");;
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("A valid email is required");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone Number is required")
            .Matches("^\\+90[0-9]{10}$").WithMessage("A valid phone number is required");

        RuleFor(x => x.Password)    
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("New password must be at least 6 characters.");

        RuleFor(x => x.RePassword)
            .NotEmpty().WithMessage("RePassword is required")
            .Equal(x => x.RePassword).WithMessage("Passwords do not match");
    }
}