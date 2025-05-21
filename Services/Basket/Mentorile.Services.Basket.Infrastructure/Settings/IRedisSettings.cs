namespace Mentorile.Services.Basket.Infrastructure.Settings;
public interface IRedisSettings
{
    public string Host { get; set; }    
    public int Port { get; set; }    
}