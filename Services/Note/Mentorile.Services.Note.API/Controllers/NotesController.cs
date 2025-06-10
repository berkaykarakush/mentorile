using MediatR;
using Mentorile.Services.Note.Application.Commands;
using Mentorile.Services.Note.Application.Queries;
using Mentorile.Shared.Common;
using Mentorile.Shared.ControllerBases;
using Mentorile.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mentorile.Services.Note.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class NotesController : CustomControllerBase
{
    private readonly IMediator _mediator;
    private readonly ISharedIdentityService _sharedIdentityService;
    public NotesController(IMediator mediator, ISharedIdentityService sharedIdentityService)
    {
        _mediator = mediator;
        _sharedIdentityService = sharedIdentityService;
    }

    /// <summary>
    /// Get all notes with pagination (Admin)
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery]PagingParams pagingParams, CancellationToken cancellationToken)
        => CreateActionResultInstance(await _mediator.Send(new GetAllNotesQuery(pagingParams), cancellationToken));

    /// <summary>
    /// Get a specific note by Id
    /// </summary>
    [HttpGet("{noteId:guid}")]
    public async Task<IActionResult> GetById(Guid noteId, CancellationToken cancellationToken)
        => CreateActionResultInstance(await _mediator.Send(new GetNoteByIdQuery(noteId), cancellationToken));

    /// <summary>
    /// Get all notes created by authenticated user
    /// </summary>
    [HttpGet("me")]
    public async Task<IActionResult> GetMyNotes([FromQuery]PagingParams pagingParams, CancellationToken cancellationToken)
        => CreateActionResultInstance(await _mediator.Send(new GetAllNotesByUserIdQuery(pagingParams, _sharedIdentityService.GetUserId), cancellationToken));

    /// <summary>
    /// Create a new note for authenticated user
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateNoteCommand command, CancellationToken cancellationToken)
    {
        command.UserId = _sharedIdentityService.GetUserId;
        return CreateActionResultInstance(await _mediator.Send(command, cancellationToken));
    }

    /// <summary>
    /// Update a note owned by the authenticated user
    /// </summary>
    [HttpPut("update/{noteId:guid}")]
    public async Task<IActionResult> Update(Guid noteId, [FromBody] UpdateNoteCommand command, CancellationToken cancellationToken)
    {
        command.UserId = _sharedIdentityService.GetUserId;
        if (noteId != command.Id)
            return BadRequest("The noteId in the route does not match the one in the body.");

        return CreateActionResultInstance(await _mediator.Send(command, cancellationToken));
    }

    /// <summary>
    /// Delete a note owned by authenticated user
    /// </summary>
    [HttpDelete("delete/{noteId:guid}")]
    public async Task<IActionResult> Delete(Guid noteId, CancellationToken cancellationToken)
        => CreateActionResultInstance(await _mediator.Send(new DeleteNoteCommand() { NoteId = noteId, UserId = _sharedIdentityService.GetUserId }, cancellationToken));
}