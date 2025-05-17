using MediatR;
using Mentorile.Services.PhotoStock.Application.Commands;
using Mentorile.Services.PhotoStock.Domain.Interfaces;
using Mentorile.Shared.Common;

namespace Mentorile.Services.PhotoStock.Application.CommandHandlers;
public class DeletePhotoCommandHandler : IRequestHandler<DeletePhotoCommand, Result<bool>>
{
    private readonly IPhotoRepository _photoRepository;

    public DeletePhotoCommandHandler(IPhotoRepository photoRepository)
    {
        _photoRepository = photoRepository;
    }

    public async Task<Result<bool>> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
    {
        var result = await _photoRepository.SoftDeleteAsync(request.PhotoId);
        if (!result.IsSuccess) return Result<bool>.Failure("Failed to photo delete.");
        return Result<bool>.Success(result.IsSuccess, "Photo deleted successfully.");
    }
}