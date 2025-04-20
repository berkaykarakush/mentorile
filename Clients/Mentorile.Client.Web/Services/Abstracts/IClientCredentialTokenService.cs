namespace Mentorile.Client.Web.Services.Abstracts;
public interface IClientCredentialTokenService
{
    Task<string> GetTokenAsync();
}