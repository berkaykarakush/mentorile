using MediatR;
using Mentorile.Services.Course.Application.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Course.Application.Queries;
public class GetCourseByIdQuery : IRequest<Result<CourseDTO>>
{
    public string Id { get; set; }
}