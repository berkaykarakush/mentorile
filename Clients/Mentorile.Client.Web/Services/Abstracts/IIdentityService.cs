using Duende.IdentityModel.Client;
using Mentorile.Client.Web.Models;
using Mentorile.Shared.Common;

namespace Mentorile.Client.Web.Services.Abstracts;
public interface IIdentityService
{
    Task<Result<bool>>  SignIn(SignInInput signInInput);
    Task<TokenResponse> GetAccessTokenByRefreshToken();
    Task RevokeRefreshToken();   
}