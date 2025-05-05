using MediatR;
using Mentorile.Services.Course.Application.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Course.Application.Queries;
public class GetAllCoursesQuery : IRequest<Result<PagedResult<CourseDTO>>>
{
    public PagingParams? PagingParams { get; set; }   
}