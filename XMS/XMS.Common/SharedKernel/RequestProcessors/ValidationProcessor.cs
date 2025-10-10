using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using XMS.Common.SharedKernel.Abstractions;

namespace XMS.Common.SharedKernel.RequestProcessors;

public class ValidationProcessor<TRequest, TResult>(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationProcessor<TRequest, TResult>> logger) : IRequestProcessor<TRequest, TResult>
    where TRequest : IRequest<TResult>
    where TResult : IResult
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;
    private readonly ILogger<ValidationProcessor<TRequest, TResult>> _logger = logger;

    public async Task<TResult> ProcessAsync(TRequest request, Func<Task<TResult>> next, CancellationToken cancellationToken = default)
    {
        var requestType = typeof(TRequest).Name;

        _logger.LogDebug("{RequestType} Validation", requestType);

        var validationTasks = _validators.Select(v => v.ValidateAsync(request, cancellationToken));

        var validationResults = await Task.WhenAll(validationTasks);

        var failures = validationResults
            .Where(result => !result.Equals(ValidationResult.Success))
            .ToList();

        if (failures.Count != 0)
        {
            _logger.LogWarning("{RequestType} Validation errors {@Request} {@ValidationErrors}", requestType, request, failures);

            throw new ValidationException($"Validation Errors {requestType} {JsonSerializer.Serialize(failures)}");
        }

        _logger.LogDebug("{RequestType} Validated", requestType);

        return await next();
    }
}
