namespace Mentorile.IdentityServer.Services;

public interface IUserAccessService
{
    Task<(bool isGranted, string reason)> CheckAccessAsync(string userId, string target);
}