using MediatR;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Note.Application.Commands;
public class UpdateNoteCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
    public string? UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}