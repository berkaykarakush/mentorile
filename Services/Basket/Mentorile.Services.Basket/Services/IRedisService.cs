using Mentorile.Shared.Common;

namespace Mentorile.Services.Basket.Services;
public interface IRedisService
{
    Task<Result<string>> GetBasketIdForUserAsync(string userId);
    Task<Result<bool>> SetStringAsync(string key, string value, TimeSpan? expiry = null);
    Task<Result<string>> GetStringAsync(string key);
    Task<Result<bool>> RemoveKeyAsync(string key);
    Task<Result<bool>> KeyExistAsync(string key);
    Task<List<string>> GetKeysAsync(string pattern);
}