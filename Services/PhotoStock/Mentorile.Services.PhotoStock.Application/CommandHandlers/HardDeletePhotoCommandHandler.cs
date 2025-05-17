using MassTransit;
using MediatR;
using Mentorile.Services.PhotoStock.Abstractions;
using Mentorile.Services.PhotoStock.Application.Commands;
using Mentorile.Services.PhotoStock.Domain.Interfaces;
using Mentorile.Services.PhotoStock.Infrastructure.Persistence;
using Mentorile.Shared.Common;
using Microsoft.EntityFrameworkCore.Storage;

namespace Mentorile.Services.PhotoStock.Application.CommandHandlers;
public class HardDeletePhotoCommandHandler : IRequestHandler<HardDeletePhotoCommand, Result<bool>>
{
    private readonly IPhotoRepository _photoRepository;
    private readonly ICloudStorageService _cloudStorageService;
    private readonly AppDbContext _appDbContext;

    public HardDeletePhotoCommandHandler(IPhotoRepository photoRepository, ICloudStorageService cloudStorageService, AppDbContext appDbContext)
    {
        _photoRepository = photoRepository;
        _cloudStorageService = cloudStorageService;
        _appDbContext = appDbContext;
    }

    public async Task<Result<bool>> Handle(HardDeletePhotoCommand request, CancellationToken cancellationToken)
    {
        await using IDbContextTransaction transaction = await _appDbContext.Database.BeginTransactionAsync();
        try
        {
            var result = await _photoRepository.HardDeleteAsync(request.PhotoId);
            if (!result.IsSuccess) return Result<bool>.Failure("Failed to delete photo from database.");

            var cloudResult = await _cloudStorageService.DeleteAsync(request.PhotoId);
            if (!cloudResult) return Result<bool>.Failure("Failed to delete photo from cloud storage.");

            // her iki silme islemi de basariliysa commit et
            await transaction.CommitAsync();
            return Result<bool>.Success(true, "Photo deleted successfully.");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return Result<bool>.Failure($"Deletion failed: {ex.Message}");
        }
    }
}