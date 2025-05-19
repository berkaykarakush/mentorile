namespace Mentorile.Services.PhotoStock.Infrastructure.Settings;

public class GoogleDriveSettings : IGoogleDriveSettings
{
    public string AppFolderName { get; set; }
    public string ApplicationName { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string Username { get; set; }
    public string CredentialFilePath { get; set; }    
    public string AppRootFolderId { get; set; }    
}