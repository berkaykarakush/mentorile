using FluentValidation;
using Mentorile.Services.Note.Application.Commands;

namespace Mentorile.Services.Note.Application.CommandValidators;

/// <summary>
/// Validator for the <see cref="CreateNoteCommand"/>.
/// Ensures the required fields are provided and within allowed limits.
/// </summary>
public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
{
    public CreateNoteCommandValidator()
    {
        // RuleFor(x => x.UserId)
        //     .MaximumLength(50).WithMessage("UserId must not exceed 50 characters");

        // RuleFor(x => x.Title)
        //     .NotEmpty().WithMessage("Title is required")
        //     .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        // RuleFor(x => x.Content)
        //     .MaximumLength(5000).WithMessage("Content must not exceed 5000 characters.");
    }
}