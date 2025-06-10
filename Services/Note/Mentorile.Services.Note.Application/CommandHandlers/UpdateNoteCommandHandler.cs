using MediatR;
using Mentorile.Services.Note.Application.Commands;
using Mentorile.Services.Note.Domain.Interfaces;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Note.Application.CommandHandlers;

/// <summary>
/// 
/// </summary>
public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, Result<bool>>
{
    private readonly INoteRepository _noteRepository;

    public UpdateNoteCommandHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Result<bool>> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var noteResult = await _noteRepository.GetNoteByIdAsync(request.Id, cancellationToken);
        if (!noteResult.IsSuccess || noteResult.Data == null)
            return Result<bool>.Failure("Note not found.");

        var note = noteResult.Data;
        if (note.UserId != request.UserId)
            throw new UnauthorizedAccessException("You are not authorized to update this note.");

        note.Update(request.Title, request.Content);

        var updatedResult = await _noteRepository.UpdateAsync(note, cancellationToken);
        if (!updatedResult.IsSuccess)
            return Result<bool>.Failure(updatedResult.Message);

        return Result<bool>.Success(true);
    }
}