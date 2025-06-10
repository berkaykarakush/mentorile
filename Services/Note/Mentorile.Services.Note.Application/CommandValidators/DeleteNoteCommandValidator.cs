using FluentValidation;
using Mentorile.Services.Note.Application.Commands;

namespace Mentorile.Services.Note.Application.CommandValidators;

public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
{
    public DeleteNoteCommandValidator()
    {
        // RuleFor(x => x.NoteId)
        //     .NotEmpty().WithMessage("NoteId is required");

        // RuleFor(x => x.UserId)
        //     .MaximumLength(50).WithMessage("UserId must not exceed 50 characters.");
    }
}