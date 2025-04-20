
using System.Net;
using System.Net.Http.Headers;
using Duende.AccessTokenManagement;
using Mentorile.Client.Web.Exceptions;
using Mentorile.Client.Web.Services.Abstracts;

namespace Mentorile.Client.Web.Handlers;
public class ClientCredentialsTokenHandler : DelegatingHandler
{
    private readonly IClientCredentialTokenService _clientCredentialTokenService;

    public ClientCredentialsTokenHandler(IClientCredentialTokenService clientCredentialTokenService)
    {
        _clientCredentialTokenService = clientCredentialTokenService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _clientCredentialTokenService.GetTokenAsync());

        var response = await base.SendAsync(request, cancellationToken);
        if(response.StatusCode == HttpStatusCode.Unauthorized) throw new UnAuthorizeException();
        return response;
    }
}