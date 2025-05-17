using Mentorile.Services.PhotoStock.Domain.Entities;
using Mentorile.Services.PhotoStock.Domain.Exceptions;
using Mentorile.Services.PhotoStock.Domain.Interfaces;
using Mentorile.Services.PhotoStock.Infrastructure.Persistence;
using Mentorile.Shared.Common;
using Mentorile.Shared.Services.Abstracts;

namespace Mentorile.Services.PhotoStock.Infrastructure.Repositories;
public class PhotoRepository : IPhotoRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IExecutor _executor;
    public PhotoRepository(AppDbContext appDbContext, IExecutor executor)
    {
        _appDbContext = appDbContext;
        _executor = executor;
    }

    public async Task<Result<Photo>> CreateAsync(Photo photo)
        => await _executor.ExecuteAsync(async () => 
        {
            await _appDbContext.Photos.AddAsync(photo);
            var result = await _appDbContext.SaveChangesAsync() > 0;
            if(!result) throw new PhotoNotFoundException();
            return photo;
        });

    public async Task<Result<Photo>> GetByIdAsync(string photoId)
        => await _executor.ExecuteAsync(async () => 
        {
            var photo = await _appDbContext.Photos.FindAsync(photoId);
            if(photo == null) throw new PhotoNotFoundException();
            return photo;
        });

    public async Task<Result<bool>> HardDeleteAsync(string photoId)
        => await _executor.ExecuteAsync(async () => 
        {
            var photo = await _appDbContext.Photos.FindAsync(photoId);
            if(photo == null) throw new PhotoNotFoundException();

            _appDbContext.Photos.Remove(photo);
            var result = await _appDbContext.SaveChangesAsync() > 0;
            if(!result) throw new PhotoDeleteException();
            return result;
        }); 

    public async Task<Result<bool>> SoftDeleteAsync(string photoId)
        => await _executor.ExecuteAsync(async () => 
        {
            var photo = await _appDbContext.Photos.FindAsync(photoId);
            if(photo == null) throw new PhotoNotFoundException();
            photo.Delete();
            _appDbContext.Photos.Update(photo);
            var result = await _appDbContext.SaveChangesAsync() > 0;
            if(!result) throw new PhotoDeleteException();
            return result;
        });
}