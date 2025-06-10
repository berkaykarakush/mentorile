using MediatR;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Note.Application.Commands;
public class CreateNoteCommand : IRequest<Result<bool>>
{
    public string? UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}