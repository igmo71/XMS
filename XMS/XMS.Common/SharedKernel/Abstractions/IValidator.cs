using System.ComponentModel.DataAnnotations;

namespace XMS.Common.SharedKernel.Abstractions;

public interface IValidator<TRequest>
{
    Task<ValidationResult> ValidateAsync(TRequest? request, CancellationToken cancellation = default);
}

