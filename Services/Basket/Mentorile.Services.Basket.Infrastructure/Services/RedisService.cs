using Mentorile.Services.Basket.Infrastructure.Settings;
using Mentorile.Shared.Abstractions;
using Mentorile.Shared.Common;
using StackExchange.Redis;

namespace Mentorile.Services.Basket.Infrastructure.Services;
public class RedisService : IRedisService
{
    private readonly IRedisSettings _redisSettings;
    private ConnectionMultiplexer _connectionMultiplexer;

    public RedisService(IRedisSettings redisSettings, ConnectionMultiplexer connectionMultiplexer)
    {
        _redisSettings = redisSettings;
        _connectionMultiplexer = connectionMultiplexer;
    }

    public void Connect() => _connectionMultiplexer = ConnectionMultiplexer.Connect($"{_redisSettings.Host}:{_redisSettings.Port}");

    public async Task<Result<string>> GetBasketIdForUserAsync(string userId)
    {
        try
        {
            var db = _connectionMultiplexer.GetDatabase();
            var key = $"user:{userId}:basketId";
            var basketId = await db.StringGetAsync(key);
            return basketId.HasValue ? Result<string>.Success(basketId.ToString()) : Result<string>.Success(string.Empty);
        }
        catch (Exception ex)
        {
            return Result<string>.Failure($"Error retrieving basketId for user: {ex.Message}");
        }
    }

    public async Task<Result<bool>> SetStringAsync(string key, string value, TimeSpan? expiry = null)
    {
        try
        {
            var db = _connectionMultiplexer.GetDatabase();
            bool result = await db.StringSetAsync(key, value, expiry);
            return result ? Result<bool>.Success(true) : Result<bool>.Failure("Failed to set value in Redis.");
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"Error setting value in Redis: {ex.Message}");
        }
    }

    public async Task<Result<string>> GetStringAsync(string key)
    {
        try
        {
            var db = _connectionMultiplexer.GetDatabase();
            var value = await db.StringGetAsync(key);
            return value.HasValue ? Result<string>.Success(value.ToString()) : Result<string>.Failure("Key not found.");
        }
        catch (Exception ex)
        {
            return Result<string>.Failure($"Error getting value from Redis: {ex.Message}");
        }
    }

    public async Task<Result<bool>> RemoveKeyAsync(string key)
    {
        try
        {
            var db = _connectionMultiplexer.GetDatabase();
            bool deleted = await db.KeyDeleteAsync(key);
            return deleted ? Result<bool>.Success(true) : Result<bool>.Failure("Failed to delete key from Redis.");
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"Error removing key from Redis: {ex.Message}");
        }
    }

    public async Task<Result<bool>> KeyExistAsync(string key)
    {
        try
        {
            var db = _connectionMultiplexer.GetDatabase();
            bool exists = await db.KeyExistsAsync(key);
            return exists ? Result<bool>.Success(true) : Result<bool>.Failure("Key does not exist.");
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"Error checking if key exists: {ex.Message}");
        }
    }

    public async Task<List<string>> GetKeysAsync(string pattern)
    {
        var endpoints = _connectionMultiplexer.GetEndPoints();
        var server = _connectionMultiplexer.GetServer(endpoints.First());

        var keys = server.Keys(pattern: pattern).Select(k => k.ToString()).ToList();
        return await Task.FromResult(keys);
    }
}