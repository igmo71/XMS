using Microsoft.Extensions.Logging;
using System.Diagnostics;
using XMS.Common.SharedKernel.Abstractions;

namespace XMS.Common.SharedKernel.RequestProcessors;

public class LoggingProcessor<TRequest, TResult>(ILogger<LoggingProcessor<TRequest, TResult>> logger) : IRequestProcessor<TRequest, TResult>
    where TRequest : IRequest<TResult>
    where TResult : IResult
{
    private readonly ILogger<LoggingProcessor<TRequest, TResult>> _logger = logger;

    public async Task<TResult> ProcessAsync(TRequest request, Func<Task<TResult>> next, CancellationToken ct)
    {
        var requestType = typeof(TRequest).Name;

        _logger.LogDebug("{RequestType} Handling {@Request}", requestType, request);

        var timestamp = Stopwatch.GetTimestamp();

        var result = await next();

        var elapsed = Stopwatch.GetElapsedTime(timestamp).TotalMilliseconds;

        _logger.LogDebug("{RequestType} Hamdled {@Request} {@Result} in {Elapsed}ms", requestType, request, result, elapsed);

        return result;
    }
}
