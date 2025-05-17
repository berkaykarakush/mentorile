using AutoMapper;
using MediatR;
using Mentorile.Services.PhotoStock.Abstractions;
using Mentorile.Services.PhotoStock.Application.Commands;
using Mentorile.Services.PhotoStock.Application.DTOs;
using Mentorile.Services.PhotoStock.Domain.Entities;
using Mentorile.Services.PhotoStock.Domain.Interfaces;
using Mentorile.Shared.Common;
using Mentorile.Shared.Services;

namespace Mentorile.Services.PhotoStock.Application.CommandHandlers;
public class UploadPhotoCommandHandler : IRequestHandler<UploadPhotoCommand, Result<PhotoDTO>>
{
    private readonly ICloudStorageService _cloudStorageService;
    private readonly IPhotoRepository _photoRepository;
    private readonly ISharedIdentityService _sharedIdentityService;
    private readonly IMapper _mapper;
    public UploadPhotoCommandHandler(ICloudStorageService cloudStorageService, IPhotoRepository photoRepository, ISharedIdentityService sharedIdentityService, IMapper mapper)
    {
        _cloudStorageService = cloudStorageService;
        _photoRepository = photoRepository;
        _sharedIdentityService = sharedIdentityService;
        _mapper = mapper;
    }

    public async Task<Result<PhotoDTO>> Handle(UploadPhotoCommand request, CancellationToken cancellationToken)
    {
        var uploadResult = await _cloudStorageService.UploadAsync(request.Content, request.FileName, request.ContentType);
        if(string.IsNullOrEmpty(uploadResult?.Uri)) return Result<PhotoDTO>.Failure("Failed to upload photo.");

        var photo = new Photo()
        {
            UserId = _sharedIdentityService.GetUserId,
            FileName = request.FileName,
            IsPublic = request.IsPublic,
            StoragePath = uploadResult.StoragePath,
            PublicUri = request.IsPublic ? uploadResult.Uri : null,
            ContentType = request.ContentType
        };
        
        var result = await _photoRepository.CreateAsync(photo);
        if(!result.IsSuccess)
        {
            await _cloudStorageService.DeleteAsync(uploadResult.StoragePath);
            return Result<PhotoDTO>.Failure("Failed to upload photo");
        } 
        var dto = _mapper.Map<PhotoDTO>(result.Data);
        return Result<PhotoDTO>.Success(dto, "Photo uploaded successfully.");
    }
}