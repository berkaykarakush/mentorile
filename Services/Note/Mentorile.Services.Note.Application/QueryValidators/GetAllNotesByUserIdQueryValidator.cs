using FluentValidation;
using Mentorile.Services.Note.Application.Queries;

namespace Mentorile.Services.Note.Application.QueryValidators;
public class GetAllNotesByUserIdQueryValidator : AbstractValidator<GetAllNotesByUserIdQuery>
{
    public GetAllNotesByUserIdQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");
    }    
}