using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Mentorile.IdentityServer;

public static class Config
{
    public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
    {
        new ApiResource("resource_course"){Scopes = {"course_fullpermission"}},
        new ApiResource("resource_topic"){Scopes = {"topic_fullpermission"}},
        new ApiResource("resource_photo_stock") {Scopes = {"photo_stock_fullpermission"}},
        new ApiResource("resource_basket") {Scopes = {"basket_fullpermission"}},
        new ApiResource(IdentityServerConstants.LocalApi.ScopeName),
    };
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.Email(),
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource() 
            {
                Name = "roles",
                DisplayName = "Roles",
                Description = "User roles",
                UserClaims = new [] {"role"}
            }
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("course_fullpermission", "Course API full access"),
            new ApiScope("topic_fullpermission", "Topic API full access"),
            new ApiScope("photo_stock_fullpermission", "Photo Stock API full access"),
            new ApiScope("basket_fullpermission", "Basket API full access"),
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client()
            {
                ClientName = "MentorileAspNetCoreMvc",
                ClientId = "MentorileWebMVCClient",
                ClientSecrets = {new Secret("secret".Sha256())},
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = {
                    "course_fullpermission",
                    "topic_fullpermission",
                    "photo_stock_fullpermission",
                    IdentityServerConstants.LocalApi.ScopeName
                }
            },
            new Client()
            {
                ClientName = "MentorileAspNetCoreMvc",
                ClientId = "MentorileWebMVCClientForUser",
                ClientSecrets = {new Secret("secret".Sha256())},
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowOfflineAccess = true,
                AllowedScopes = {
                    "basket_fullpermission",
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    IdentityServerConstants.LocalApi.ScopeName,
                    "roles"
                },
                AccessTokenLifetime = 1*60*60,
                RefreshTokenExpiration = TokenExpiration.Absolute,
                AbsoluteRefreshTokenLifetime = (int)(DateTime.UtcNow.AddDays(60) - DateTime.UtcNow).TotalSeconds,
                RefreshTokenUsage = TokenUsage.ReUse
            }            
        };
}