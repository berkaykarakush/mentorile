using Mentorile.IdentityServer.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.IdentityServer.Services;

public interface IAuthService
{
    Task<Result<UserAuthenticatedDto>> RegisterAsync(string name, string surname, string email, string phoneNumber, string password);
    Task<Result<Guid>> AuthenticateAsync(string identifier, string password);
    Task<Result<bool>> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
    Task<Result<bool>> ConfirmEmailAsync(string userId, string confirmationCode);
    Task<Result<bool>> ResendConfirmEmailAsync(string userId);
}