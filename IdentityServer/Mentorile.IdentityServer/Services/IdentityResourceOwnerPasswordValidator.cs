using Duende.IdentityServer.Validation;
using IdentityModel;

namespace Mentorile.IdentityServer.Services;
public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
{
    private readonly IAuthService _authService;

    public IdentityResourceOwnerPasswordValidator(IAuthService authService)
    {
        _authService = authService;
    }
    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var user = await _authService.AuthenticateAsync(context.UserName, context.Password);
        context.Result = new GrantValidationResult(user.Data.ToString(), OidcConstants.AuthenticationMethods.Password);
    }
}