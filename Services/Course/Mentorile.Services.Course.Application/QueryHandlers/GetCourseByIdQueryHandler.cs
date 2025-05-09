using AutoMapper;
using MediatR;
using Mentorile.Services.Course.Application.DTOs;
using Mentorile.Services.Course.Application.Queries;
using Mentorile.Services.Course.Domain.Interfaces;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Course.Application.QueryHandlers;
public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, Result<CourseDTO>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;

    public GetCourseByIdQueryHandler(ICourseRepository courseRepository, IMapper mapper)
    {
        _courseRepository = courseRepository;
        _mapper = mapper;
    }

    public async Task<Result<CourseDTO>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(request.Id);
        if(course == null) return Result<CourseDTO>.Failure("Course not found.");

        var dto = _mapper.Map<CourseDTO>(course.Data);
        return Result<CourseDTO>.Success(dto);
    }
}