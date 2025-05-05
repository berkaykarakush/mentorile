using AutoMapper;
using MediatR;
using Mentorile.Services.Course.Application.DTOs;
using Mentorile.Services.Course.Application.Queries;
using Mentorile.Services.Course.Domain.Interfaces;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Course.Application.QueryHandlers;
public class GetAllCoursesByUserIdQueryHandler : IRequestHandler<GetAllCoursesByUserIdQuery, Result<PagedResult<CourseDTO>>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;

    public GetAllCoursesByUserIdQueryHandler(ICourseRepository courseRepository, IMapper mapper)
    {
        _courseRepository = courseRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<CourseDTO>>> Handle(GetAllCoursesByUserIdQuery request, CancellationToken cancellationToken)
    {
        if(request.PagingParams == null) request.PagingParams = new PagingParams();
        var courses = await _courseRepository.GetAllByUserIdAsync(request.UserId, request.PagingParams);
        if(courses == null) return Result<PagedResult<CourseDTO>>.Failure("Courses not found.");

        var dtos = _mapper.Map<List<CourseDTO>>(courses.Data.Data);
        if(dtos == null) return Result<PagedResult<CourseDTO>>.Failure("Mapping failed.");

        var paged = PagedResult<CourseDTO>.Create(dtos, courses.Data.TotalCount, request.PagingParams);
        return Result<PagedResult<CourseDTO>>.Success(paged);
    }
}