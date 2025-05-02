using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Mentorile.Shared.Behaviors;
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var stopWatch = Stopwatch.StartNew();
        _logger.LogInformation($"[START] Handling {requestName}: {request}");
        try
        {

            var response = await next();
            stopWatch.Stop();
            _logger.LogInformation($"[END] Handled {requestName} in {stopWatch.ElapsedMilliseconds} ms");
            return response;
        }
        catch (Exception ex)
        {
            stopWatch.Stop();
            _logger.LogError(ex, $"[ERROR] Handling {requestName} failed after {stopWatch.ElapsedMilliseconds} ms");
            throw;
        }
    }
}