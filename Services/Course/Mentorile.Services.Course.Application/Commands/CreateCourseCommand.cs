using MediatR;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Course.Application.Commands;
public class CreateCourseCommand : IRequest<Result<bool>>
{
    public string Name { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string? PhotoUri { get; set; }
}