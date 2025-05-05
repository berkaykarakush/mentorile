using MediatR;
using Mentorile.Services.Course.Application.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Course.Application.Queries;
public class GetAllCoursesByUserIdQuery : IRequest<Result<PagedResult<CourseDTO>>>
{
    public PagingParams? PagingParams { get; set; }
    public string UserId { get; set; }
}