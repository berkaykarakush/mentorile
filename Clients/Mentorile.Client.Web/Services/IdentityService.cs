using System.Globalization;
using System.Security.Claims;
using Duende.IdentityModel.Client;
using Mentorile.Client.Web.Models;
using Mentorile.Client.Web.Services.Abstracts;
using Mentorile.Client.Web.Settings;
using Mentorile.Shared.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Mentorile.Client.Web.Services;
public class IdentityService : IIdentityService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IClientSettings _clientSettings;
    private readonly IServiceApiSettings _serviceApiSettings;

    public IdentityService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IClientSettings clientSettings, IServiceApiSettings serviceApiSettings)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
        _clientSettings = clientSettings;
        _serviceApiSettings = serviceApiSettings;
    }

    public async Task<TokenResponse> GetAccessTokenByRefreshToken()
    {
        var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest{
            Address = _serviceApiSettings.IdentityBaseUri,
            Policy = new DiscoveryPolicy { RequireHttps = false }
        });

        if (discovery.IsError || string.IsNullOrEmpty(discovery.TokenEndpoint))
            throw new Exception($"Discovery endpoint error: {discovery.Error ?? "Token endpoint not found"}", discovery.Exception);

        var refreshToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);
        if (string.IsNullOrEmpty(refreshToken))
            throw new Exception("Refresh token is null or empty");

        var refreshTokenRequest = new RefreshTokenRequest(){
            ClientId = _clientSettings.ResourceOwnerPassword.ClientId,
            ClientSecret = _clientSettings.ResourceOwnerPassword.ClientSecret,
            RefreshToken = refreshToken,
            Address = discovery.TokenEndpoint
        }; 

        var token = await _httpClient.RequestRefreshTokenAsync(refreshTokenRequest);
        if (token == null)
            throw new Exception("Token response is null");
        
        if(token.IsError)
            throw new Exception($"Refresh token error: {token.ErrorDescription ?? token.Error}", token.Exception);

        var authenticationTokens = new List<AuthenticationToken>(){
            new AuthenticationToken() { Name = OpenIdConnectParameterNames.AccessToken, Value = token.AccessToken }, 
            new AuthenticationToken() { Name = OpenIdConnectParameterNames.RefreshToken, Value = token.RefreshToken }, 
            new AuthenticationToken() { Name = OpenIdConnectParameterNames.ExpiresIn, Value = DateTime.UtcNow.AddSeconds(token.ExpiresIn).ToString("o", CultureInfo.InvariantCulture) }, 
        };
        
        var authenticationResult = await _httpContextAccessor.HttpContext.AuthenticateAsync();
        var properties = authenticationResult?.Properties ?? new AuthenticationProperties();
        properties.StoreTokens(authenticationTokens);

        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, authenticationResult.Principal, properties);

        return token;
    }

    public async Task RevokeRefreshToken()
    {
        var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest{
            Address = _serviceApiSettings.IdentityBaseUri,
            Policy = new DiscoveryPolicy { RequireHttps = false}
        }); 

        if(discovery.IsError) throw discovery.Exception;

        var refreshToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);
        var tokenRevocationRequest = new TokenRevocationRequest(){
            ClientId = _clientSettings.ResourceOwnerPassword.ClientId,
            ClientSecret = _clientSettings.ResourceOwnerPassword.ClientSecret,
            Address = discovery.RevocationEndpoint,
            Token = refreshToken,
            TokenTypeHint = "refresh_token"
        };
        await _httpClient.RevokeTokenAsync(tokenRevocationRequest);
    }

    public async Task<Result<bool>> SignIn(SignInInput signInInput)
    {
        var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest{
            Address = _serviceApiSettings.IdentityBaseUri,
            Policy = new DiscoveryPolicy { RequireHttps = false}
        }); 

        if(discovery.IsError) throw discovery.Exception;

        var passwordTokenRequest = new PasswordTokenRequest(){
            ClientId = _clientSettings.ResourceOwnerPassword.ClientId,
            ClientSecret = _clientSettings.ResourceOwnerPassword.ClientSecret,
            UserName = signInInput.Email,
            Password = signInInput.Password,
            Address = discovery.TokenEndpoint
        }; 

        var token = await _httpClient.RequestPasswordTokenAsync(passwordTokenRequest);
        if(token.IsError){
            var errorMessage = !string.IsNullOrEmpty(token.ErrorDescription) 
            ? token.ErrorDescription 
            : token.Error ?? token.Raw ?? "Bilinmeyen bir hata olustu";

            return Result<bool>.Failure(errorMessage);
        }

        var userInfoRequest = new UserInfoRequest(){
            Token = token.AccessToken,
            Address = discovery.UserInfoEndpoint
        };

        var userInfo = await _httpClient.GetUserInfoAsync(userInfoRequest);
        if(userInfo.IsError) throw userInfo.Exception;

        var claimsIdentity = new ClaimsIdentity(
            userInfo.Claims,
            CookieAuthenticationDefaults.AuthenticationScheme,
            "name",
            "role"
        ); 
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        var authenitcationProperties = new AuthenticationProperties();
        authenitcationProperties.StoreTokens(new List<AuthenticationToken>(){
            new AuthenticationToken{Name = OpenIdConnectParameterNames.AccessToken, Value = token.AccessToken},
            new AuthenticationToken{Name = OpenIdConnectParameterNames.RefreshToken, Value = token.RefreshToken},
            new AuthenticationToken{
                Name = OpenIdConnectParameterNames.ExpiresIn,
                Value = DateTime.UtcNow.AddSeconds(token.ExpiresIn).ToString("o", CultureInfo.InvariantCulture)
            },
        });

        authenitcationProperties.IsPersistent = signInInput.IsRemember;
        await _httpContextAccessor.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            claimsPrincipal,
            authenitcationProperties
        );
        return Result<bool>.Success(true);
    }

    public async Task<Result<Guid>> Register(RegisterInput registerInput)
    {
        // TODO: /api/users/register degil de /api/auth/register yapmak gerekebilir
        // var response = await _httpClient.PostAsJsonAsync($"{_serviceApiSettings.IdentityBaseUri}/api/users/register", new
        var response = await _httpClient.PostAsJsonAsync($"{_serviceApiSettings.IdentityBaseUri}/api/auth/register", new
        {
            Name = registerInput.Name,
            Surname = registerInput.Surname,
            Email = registerInput.Email,
            PhoneNumber = registerInput.PhoneNumber,
            Password = registerInput.Password,
            RePassword = registerInput.RePassword
        });

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            return Result<Guid>.Failure($"Registration failed: {errorContent}");
        }

        var result = await response.Content.ReadFromJsonAsync<Result<Guid>>();
        if (result == null)
        {
            return Result<Guid>.Failure("Failed to deserialize registration response.");
        }

        return result;
    }
}