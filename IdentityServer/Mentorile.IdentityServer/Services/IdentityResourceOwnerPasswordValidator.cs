using Duende.IdentityServer.Validation;
using IdentityModel;
using Mentorile.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;

namespace Mentorile.IdentityServer.Services;
public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
{
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var existUser = await _userManager.FindByEmailAsync(context.UserName);

        if(existUser == null)
        {
            var erros = new Dictionary<string, object>();
            erros.Add("errors", new List<string>{"The email or password is incorrect."});          
            context.Result.CustomResponse = erros;
            return; 
        }

        var passwordCheck = await _userManager.CheckPasswordAsync(existUser, context.Password);
        if(passwordCheck == false)
        {
            var erros = new Dictionary<string, object>();
            erros.Add("erros", new List<string>{"The email or password is incorrect."});
            context.Result.CustomResponse = erros;
            return;
        }
        context.Result = new GrantValidationResult(existUser.Id.ToString(), OidcConstants.AuthenticationMethods.Password);

    }
}