using Mentorile.Gateway.API.Models;

namespace Mentorile.Gateway.API.Services.Abstracts;
public interface IUserAccessService
{
    Task<UserAccessProfile> GetUserAccessProfileAsync(string userId);
}

