namespace XMS.Common.SharedKernel.Abstractions;

public interface IRequestProcessor<TRequest, TResult>
    where TRequest : IRequest<TResult>
    where TResult : IResult
{
    Task<TResult> ProcessAsync(TRequest request, Func<Task<TResult>> next, CancellationToken cancellationToken = default);
}
