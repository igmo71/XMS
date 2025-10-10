using Microsoft.Extensions.Logging;
using System.Diagnostics;
using XMS.Common.SharedKernel.Abstractions;

namespace XMS.Common.SharedKernel.RequestProcessors;

public class TransactionProcessor<TCommand, TResult>(ILogger<TransactionProcessor<TCommand, TResult>> logger) : IRequestProcessor<TCommand, TResult>
    where TCommand : ICommand<TResult>
    where TResult : IResult
{
    private readonly ILogger<TransactionProcessor<TCommand, TResult>> _logger = logger;

    public async Task<TResult> ProcessAsync(TCommand command, Func<Task<TResult>> next, CancellationToken cancellationToken = default)
    {
        var commandType = typeof(TCommand).Name;

        try
        {
            // TODO: Begin Transaction

            _logger.LogDebug("{CommandType} Transaction Begin {@Request}", commandType, command);

            var timestamp = Stopwatch.GetTimestamp();

            var result = await next();

            var elapsed = Stopwatch.GetElapsedTime(timestamp).TotalMilliseconds;

            _logger.LogDebug("{CommandType} Transaction Commit {@Request} {@Result} in {Elapsed}ms", commandType, command, result, elapsed);

            // TODO: Commit Transaction

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{CommandType} Transaction Error {@Request}", commandType, command);

            throw;
        }
    }
}
