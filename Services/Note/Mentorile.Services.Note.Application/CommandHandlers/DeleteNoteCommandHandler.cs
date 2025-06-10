using MediatR;
using Mentorile.Services.Note.Application.Commands;
using Mentorile.Services.Note.Domain.Interfaces;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Note.Application.CommandHandlers;

public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, Result<bool>>
{
    private readonly INoteRepository _noteRepository;

    public DeleteNoteCommandHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<Result<bool>> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetNoteByIdAsync(request.NoteId, cancellationToken);
        if (!note.IsSuccess || note.Data == null)
            return Result<bool>.Failure("Note not found.");

        if (request.UserId != note.Data.UserId)
            return Result<bool>.Failure("You are not authorized to delete this note.");

        var result = await _noteRepository.DeleteAsync(request.NoteId, cancellationToken);
        if (!result.IsSuccess)
            return Result<bool>.Failure(result.Message);

        return Result<bool>.Success(true);
    }
}