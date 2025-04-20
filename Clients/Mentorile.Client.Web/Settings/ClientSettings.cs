namespace Mentorile.Client.Web.Settings;
public class ClientSettings : IClientSettings
{
    public Client ClientCredentials { get; set; }

    public Client ResourceOwnerPassword { get; set; }
}