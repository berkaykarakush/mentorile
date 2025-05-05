using MediatR;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Course.Application.Commands;
public class DeleteCourseCommand : IRequest<Result<bool>>
{
    public string Id { get; set; }
}