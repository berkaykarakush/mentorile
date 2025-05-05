using MediatR;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Course.Application.Commands;
public class UpdateCourseCommand : IRequest<Result<bool>>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string UserId { get; set; }
    public string PhotoUri { get; set; }
    public List<string> TopicIds { get; set; } = new();
}