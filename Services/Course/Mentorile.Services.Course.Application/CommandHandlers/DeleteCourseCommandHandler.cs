using MediatR;
using Mentorile.Services.Course.Application.Commands;
using Mentorile.Services.Course.Domain.Interfaces;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Course.Application.CommandHandlers;
public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, Result<bool>>
{
    private readonly ICourseRepository _courseRepository;

    public DeleteCourseCommandHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<Result<bool>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var result = await _courseRepository.DeleteAsync(request.Id);
        if(!result.IsSuccess) return Result<bool>.Failure("Failed to course deleted.");
        return Result<bool>.Success(result.IsSuccess, "Course deleted successfully.");
    }
}