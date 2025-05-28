namespace Mentorile.IdentityServer.DTOs;
public class UserAuthenticatedDto
{
    public string UserId { get; set; }
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public int ExpiresIn { get; set; }
    public string TokenType { get; set; } = "Bearer";

}