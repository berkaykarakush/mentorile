using Mentorile.Gateway.API.Models;

namespace Mentorile.Gateway.API.AccessControl.Abstracts;
public interface IAccessRequirement
{
    Task<bool> IsSatisfiedAsync(HttpContext context, UserAccessProfile user);
    string FailureRedirectUri { get; }
}