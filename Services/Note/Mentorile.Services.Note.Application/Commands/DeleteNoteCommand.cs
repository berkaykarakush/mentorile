using MediatR;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Note.Application.Commands;
public class DeleteNoteCommand : IRequest<Result<bool>>
{
    public Guid NoteId { get; set; }
    public string? UserId { get; set; }
}