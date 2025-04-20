namespace Mentorile.Client.Web.Settings;
public interface IClientSettings
{
    public Client ClientCredentials { get; set; }
    public Client ResourceOwnerPassword { get; set; }
}