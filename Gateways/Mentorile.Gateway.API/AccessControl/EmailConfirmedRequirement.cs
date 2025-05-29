using Mentorile.Gateway.API.AccessControl.Abstracts;
using Mentorile.Gateway.API.Models;

namespace Mentorile.Gateway.API.AccessControl;
public class EmailConfirmedRequirement : IAccessRequirement
{
    public string FailureRedirectUri => "/auth/confirmEmail";
    public async Task<bool> IsSatisfiedAsync(HttpContext context, UserAccessProfile user)
    {
        return user.EmailConfirmed;
    }
}