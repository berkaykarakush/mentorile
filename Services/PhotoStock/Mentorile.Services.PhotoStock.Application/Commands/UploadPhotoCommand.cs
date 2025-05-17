using MediatR;
using Mentorile.Services.PhotoStock.Application.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.PhotoStock.Application.Commands;

public class UploadPhotoCommand : IRequest<Result<PhotoDTO>>
{
    public byte[] Content { get; set; } = default!;
    public string FileName { get; set; } = default!;
    public string ContentType { get; set; } = default!;
    public bool IsPublic { get; set; }
}