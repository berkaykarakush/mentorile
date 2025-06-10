using FluentValidation;
using Mentorile.Services.Note.Application.Queries;

namespace Mentorile.Services.Note.Application.QueryValidators;
public class GetNoteByIdQueryValidator : AbstractValidator<GetNoteByIdQuery>
{
    public GetNoteByIdQueryValidator()
    {
        RuleFor(x => x.NoteId)
            .NotEmpty().WithMessage("NoteId is required.");
    }
}