
using Mentorile.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;

namespace Mentorile.IdentityServer.Services;

public class UserAccessService : IUserAccessService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserAccessService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<(bool isGranted, string reason)> CheckAccessAsync(string userId, string target)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return (false, "User not found.");

        if (!user.EmailConfirmed) return (false, "EmailNotConfirmed.");

        // check other access rules 

        return (true, null);
    }
}