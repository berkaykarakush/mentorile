using Mentorile.Shared.Common;

namespace Mentorile.IdentityServer.Services;
public interface IAuthService
{
    Task<Result<Guid>> RegisterAsync(string name, string surname, string email, string phoneNumber, string password);    
    Task<Result<Guid>> AuthenticateAsync(string identifier, string password);
    Task<Result<bool>> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
}