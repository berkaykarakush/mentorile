using Mentorile.Services.User.Domain.Entities;
using Mentorile.Services.User.Domain.Enums;
using Mentorile.Services.User.Domain.Exceptions;
using Mentorile.Services.User.Domain.Services;
using Mentorile.Services.User.Infrastructure.Persistence;
using Mentorile.Shared.Common;
using Mentorile.Shared.Services.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Mentorile.Services.User.Infrastructure.Repositories;
public class UserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IExecutor _executor;

    public UserRepository(AppDbContext appDbContext, IExecutor executor)
    {
        _appDbContext = appDbContext;
        _executor = executor;
    }

    public async Task<Result<bool>> AddAsync(UserProfile user)
    => await _executor.ExecuteAsync(async () =>{
        await _appDbContext.Profiles.AddAsync(user);
        var result = await _appDbContext.SaveChangesAsync() > 0;
        return result;
    });
    public async Task<Result<bool>> SoftDeleteAsync(Guid userId)
    => await _executor.ExecuteAsync(async () => {
        var user = await _appDbContext.Profiles.FindAsync(userId);
        if(user == null) throw new UserNotFoundException();
        user.Delete();
        _appDbContext.Profiles.Update(user);
        var result = await _appDbContext.SaveChangesAsync() > 0;
        return result;
    });

    public async Task<Result<bool>> HardDeleteAsync(Guid userId)
    => await _executor.ExecuteAsync(async () => {
        var user = await _appDbContext.Profiles.FindAsync(userId);
        if(user == null) throw new UserNotFoundException();
        _appDbContext.Profiles.Remove(user);
        var result = await _appDbContext.SaveChangesAsync() > 0;
        return result;
    });
    
    public async Task<Result<bool>> EmailExistsAsync(string email)
     => await _executor.ExecuteAsync(async () =>{
        var result = await _appDbContext.Profiles.AnyAsync(p => p.Email == email);
        return result;
     });
    public async Task<Result<UserProfile>> GetByEmailAsync(string email)
    => await _executor.ExecuteAsync(async () =>{
        var user = await _appDbContext.Profiles.FirstOrDefaultAsync(p => p.Email == email);
        if(user == null) throw new UserNotFoundException();
        return user;
    });

    public async Task<Result<UserProfile>> GetByIdAsync(Guid id)
    => await _executor.ExecuteAsync(async () =>{
        var user = await _appDbContext.Profiles.FindAsync(id);
        if(user == null) throw new UserNotFoundException();
        return user;
    });

    public async Task<Result<UserProfile>> GetByPhoneNumberAsync(string phoneNumber)
    => await _executor.ExecuteAsync(async () =>{
        var user = await _appDbContext.Profiles.FirstOrDefaultAsync(p => p.PhoneNumber == phoneNumber);
        if(user == null) throw new UserNotFoundException();
        return user;
    });

    public async Task<Result<bool>> PhoneNumberExistsAsync(string phoneNumber)
        => await _executor.ExecuteAsync(async () =>
        {
            var result = await _appDbContext.Profiles.AnyAsync(p => p.PhoneNumber == phoneNumber);
            return result;
        });

    public async Task<Result<bool>> UpdateAsync(UserProfile user)
    => await _executor.ExecuteAsync(async () => {
        _appDbContext.Profiles.Update(user);
        var result = await _appDbContext.SaveChangesAsync() > 0;
        return result;
    });

    public async Task<Result<PagedResult<UserProfile>>> GetAllAsync(PagingParams pagingParams)
    => await _executor.ExecuteAsync(async () =>{
        var query = _appDbContext.Profiles;
        var totalCount  = await query.CountAsync();
        var profilies = await query
                .Skip((pagingParams.PageNumber - 1) * pagingParams.PageSize)
                .Take(pagingParams.PageSize)
                .ToListAsync();
        var paged = PagedResult<UserProfile>.Create(profilies, totalCount, pagingParams);
        return paged;
    });
    public async Task<Result<PagedResult<UserProfile>>> GetAllActiveAsync(PagingParams pagingParams)
    => await _executor.ExecuteAsync(async () => {
        var query = _appDbContext.Profiles.Where(p => p.Status == UserStatus.Active);
        var totalCount = await query.CountAsync();
        var profilies = await query
            .Skip((pagingParams.PageNumber - 1) * pagingParams.PageSize)
            .Take(pagingParams.PageSize)
            .ToListAsync();
        var paged = PagedResult<UserProfile>.Create(profilies, totalCount, pagingParams);
        return paged;
    });

     public async Task<Result<PagedResult<UserProfile>>> SearchAsync(string keyword, PagingParams pagingParams)
    => await _executor.ExecuteAsync(async () => {
        var query = _appDbContext.Profiles.Where(p => 
            p.Name.Contains(keyword) || p.Surname.Contains(keyword) ||
            p.Email.Contains(keyword) || p.PhoneNumber.Contains(keyword)
        );
        var totalCount = await query.CountAsync();
        var profilies = await query
            .Skip((pagingParams.PageNumber - 1) * pagingParams.PageSize)
            .Take(pagingParams.PageSize)
            .ToListAsync();
        var paged = PagedResult<UserProfile>.Create(profilies, totalCount, pagingParams);
        return paged;
    });
}