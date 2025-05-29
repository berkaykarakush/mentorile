using System.Net.Http.Headers;
using Mentorile.Client.Web.Exceptions;
using Mentorile.Client.Web.Services.Abstracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Mentorile.Client.Web.Handlers;
public class ResourceOwnerPasswordTokenHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IIdentityService _identityService;
    private readonly ILogger<ResourceOwnerPasswordTokenHandler> _logger;

    public ResourceOwnerPasswordTokenHandler(IHttpContextAccessor httpContextAccessor, IIdentityService identityService, ILogger<ResourceOwnerPasswordTokenHandler> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _identityService = identityService;
        _logger = logger;
    }
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        
        var response = await base.SendAsync(request, cancellationToken);
        if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized){
            var tokenResponse = await _identityService.GetAccessTokenByRefreshToken();
            if(tokenResponse!=null){
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);
                response = await base.SendAsync(request, cancellationToken);
            }
        }
        if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized) throw new UnAuthorizeException();

        if(response.StatusCode == System.Net.HttpStatusCode.Forbidden) throw new EmailNotConfirmedException();

        return response;
    }
}