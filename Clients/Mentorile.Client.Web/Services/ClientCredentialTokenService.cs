using Mentorile.Client.Web.Services.Abstracts;
using Mentorile.Client.Web.Settings;
using Microsoft.Extensions.Caching.Memory;
using Duende.IdentityModel.Client;
namespace Mentorile.Client.Web.Services;
public class ClientCredentialTokenService : IClientCredentialTokenService
{
    private readonly IServiceApiSettings _serviceApiSettings;
    private readonly IClientSettings _clientSettings;
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _memoryCache;
    private const string TokenCacheKey = "ClientCredentialToken";

    public ClientCredentialTokenService(IServiceApiSettings serviceApiSettings, IClientSettings clientSettings, HttpClient httpClient, IMemoryCache memoryCache)
    {
        _serviceApiSettings = serviceApiSettings;
        _clientSettings = clientSettings;
        _httpClient = httpClient;
        _memoryCache = memoryCache;
    }

    public async Task<string> GetTokenAsync()
        {
            if (_memoryCache.TryGetValue(TokenCacheKey, out string cachedToken))
            {
                return cachedToken;
            }

            var disco = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.IdentityBaseUri,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

            if (disco.IsError)
            {
                throw disco.Exception;
            }

            var clientCredentialTokenRequest = new ClientCredentialsTokenRequest
            {
                ClientId = _clientSettings.ClientCredentials.ClientId,
                ClientSecret = _clientSettings.ClientCredentials.ClientSecret,
                Address = disco.TokenEndpoint
            };

            var newToken = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialTokenRequest);

            if (newToken.IsError)
            {
                throw newToken.Exception;
            }

            // Cache'e ekleme (token süresinden 60 saniye erken süreyle)
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(newToken.ExpiresIn - 60)
            };

            _memoryCache.Set(TokenCacheKey, newToken.AccessToken, cacheOptions);

            return newToken.AccessToken;
        }
}