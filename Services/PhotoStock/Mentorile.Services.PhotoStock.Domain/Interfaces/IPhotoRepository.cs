using Mentorile.Services.PhotoStock.Domain.Entities;
using Mentorile.Shared.Common;

namespace Mentorile.Services.PhotoStock.Domain.Interfaces;
public interface IPhotoRepository
{
    Task<Result<Photo>> CreateAsync(Photo photo);
    Task<Result<Photo>> GetByIdAsync(string photoId);
    Task<Result<bool>> SoftDeleteAsync(string photoId);
    Task<Result<bool>> HardDeleteAsync(string photoId);
}