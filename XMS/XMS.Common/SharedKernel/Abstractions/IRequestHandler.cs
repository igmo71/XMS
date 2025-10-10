namespace XMS.Common.SharedKernel.Abstractions;

public interface IRequestHandler<TRequest, TResult>
    where TRequest : IRequest<TResult>
    where TResult : IResult
{
    Task<TResult> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
}
