using System.Runtime.CompilerServices;
using Mentorile.Shared.Common;
using Mentorile.Shared.Services.Abstracts;
using Microsoft.Extensions.Logging;

namespace Mentorile.Shared.Services;
public class Executor : IExecutor
{
    private readonly ILogger<Executor> _logger;

    public Executor(ILogger<Executor> logger)
    {
        _logger = logger;
    }

    public async Task<Result<T>> ExecuteAsync<T>(Func<Task<T>> operation, [CallerMemberName] string methodName = "")
    {
        {
        try
        {
            var result = await operation();
            if(result == null && typeof(T) == null) return Result<T>.Failure("Operation returned null.");
            return Result<T>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in method {methodName}: {ex.Message}");
            return Result<T>.Failure("An unexpected error occured");
        }
    }
    }
}