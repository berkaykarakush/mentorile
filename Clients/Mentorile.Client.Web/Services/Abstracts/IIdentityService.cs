using Duende.IdentityModel.Client;
using Mentorile.Client.Web.Models;
using Mentorile.Shared.Common;

namespace Mentorile.Client.Web.Services.Abstracts;
public interface IIdentityService
{
    Task<Result<bool>>  SignIn(SignInInput signInInput);
    Task<Result<UserAuthenticatedModel>> Register(RegisterInput registerInput);
    Task<Result<bool>> ConfirmEmail(ConfirmEmailInput emailInput);
    Task<Result<bool>> ResendConfirmEmail(string userId);
    Task<TokenResponse> GetAccessTokenByRefreshToken();
    Task RevokeRefreshToken();   
}