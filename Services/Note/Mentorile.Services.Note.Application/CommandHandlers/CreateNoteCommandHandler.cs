using MediatR;
using Mentorile.Services.Note.Application.Commands;
using Mentorile.Services.Note.Domain.Interfaces;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Note.Application.CommandHandlers;

/// <summary>
/// Handles the logic for creating a new note.
/// </summary>
public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Result<bool>>
{
    private readonly INoteRepository _noteRepository;


    /// <summary>
    /// Initializes a new instance of the <see cref="CreateNoteCommandHandler"/> class 
    /// </summary>
    /// <param name="noteRepository">The repository for note persistence operations.</param>
    public CreateNoteCommandHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    /// <summary>
    /// Handles the <see cref="CreateNoteCommand"/> by creating and saving a new note. 
    /// </summary>
    /// <param name="request">The command containing note creation details.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A result indicating success or failure of the operation.</returns>
    public async Task<Result<bool>> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = new Domain.Entities.Note(request.UserId, request.Title, request.Content);

        var createResult = await _noteRepository.CreateAsync(note, cancellationToken);
        if (!createResult.IsSuccess) return Result<bool>.Failure(createResult.Message);

        return Result<bool>.Success(createResult.Data);
    }
}