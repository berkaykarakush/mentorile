using AutoMapper;
using MediatR;
using Mentorile.Services.Course.Application.Commands;
using Mentorile.Services.Course.Domain.Interfaces;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Course.Application.CommandHandlers;
public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Result<bool>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;

    public CreateCourseCommandHandler(ICourseRepository courseRepository, IMapper mapper)
    {
        _courseRepository = courseRepository;
        _mapper = mapper;
    }

    public async Task<Result<bool>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = _mapper.Map<Domain.Entities.Course>(request);
        var result = await _courseRepository.CreateCourseAsync(course);
        if(!result.IsSuccess) Result<bool>.Failure("Failed to course created.");

        return Result<bool>.Success(true, "Course created successfully.");
    }
}