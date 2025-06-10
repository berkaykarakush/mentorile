using FluentValidation;
using Mentorile.Services.Note.Application.Commands;

namespace Mentorile.Services.Note.Application.CommandValidators;
public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
{
    public UpdateNoteCommandValidator()
    {
        // RuleFor(x => x.Id)
        //     .NotEmpty().WithMessage("Note Id is required.");

        // RuleFor(x => x.UserId)
        //     .MaximumLength(50).WithMessage("UserId must not exceed 50 characters.");

        // RuleFor(x => x.Title)
        //     .NotEmpty().WithMessage("Title is required")
        //     .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        // RuleFor(x => x.Content)
        //     .MaximumLength(5000).WithMessage("Content must not exceed 5000 characters.");
    }
}