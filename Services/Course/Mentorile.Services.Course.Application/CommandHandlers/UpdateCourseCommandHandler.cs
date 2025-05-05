using AutoMapper;
using MediatR;
using Mentorile.Services.Course.Application.Commands;
using Mentorile.Services.Course.Domain.Interfaces;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Course.Application.CommandHandlers;
public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, Result<bool>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;
    public UpdateCourseCommandHandler(ICourseRepository courseRepository, IMapper mapper)
    {
        _courseRepository = courseRepository;
        _mapper = mapper;
    }

    public async Task<Result<bool>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = _mapper.Map<Domain.Entities.Course>(request);
        var result = await _courseRepository.UpdateCourseAsync(course);
        if (!result.IsSuccess) return Result<bool>.Failure("Failed to course updated.");
        return Result<bool>.Success(result.IsSuccess, "Course updated successfully.");
    }
}