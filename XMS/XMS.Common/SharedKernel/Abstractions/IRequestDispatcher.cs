namespace XMS.Common.SharedKernel.Abstractions;

public interface IRequestDispatcher
{
    Task<TResult> SendAsync<TRequest, TResult>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IRequest<TResult>
        where TResult : IResult;
}
