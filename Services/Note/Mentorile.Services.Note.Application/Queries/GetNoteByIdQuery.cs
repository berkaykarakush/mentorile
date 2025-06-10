using MediatR;
using Mentorile.Services.Note.Application.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Note.Application.Queries;

public class GetNoteByIdQuery : IRequest<Result<NoteDTO>>
{
    public Guid NoteId { get; set; }    
    public GetNoteByIdQuery(Guid noteId)
    {
        NoteId = noteId;
    }
}