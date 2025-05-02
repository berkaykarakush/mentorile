using Mentorile.Services.User.Domain.Entities;
using Mentorile.Shared.Common;

namespace Mentorile.Services.User.Domain.Services;
public interface IUserRepository 
{
    Task<Result<UserProfile>> GetByIdAsync(Guid id);
    Task<Result<UserProfile>> GetByEmailAsync(string email);
    Task<Result<UserProfile>> GetByPhoneNumberAsync(string phoneNumber);
    Task<Result<PagedResult<UserProfile>>> GetAllAsync(PagingParams pagingParams);
    Task<Result<PagedResult<UserProfile>>> GetAllActiveAsync(PagingParams pagingParams);
    Task<Result<PagedResult<UserProfile>>> SearchAsync(string keyword, PagingParams pagingParams);
    Task<Result<bool>> EmailExistsAsync(string email);
    Task<Result<bool>> PhoneNumberExistsAsync(string phoneNumber);
    Task<Result<bool>> AddAsync(UserProfile user);
    Task<Result<bool>> UpdateAsync(UserProfile user);
    Task<Result<bool>> SoftDeleteAsync(Guid userId);
    Task<Result<bool>> HardDeleteAsync(Guid userId);
}
