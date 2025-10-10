using Microsoft.Extensions.DependencyInjection;
using XMS.Common.SharedKernel.Abstractions;

namespace XMS.Common.SharedKernel;

public class RequestDispatcher(IServiceProvider serviceProvider) : IRequestDispatcher
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task<TResult> SendAsync<TRequest, TResult>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IRequest<TResult>
        where TResult : IResult
    {
        var tRequestName = typeof(TRequest).Name;
        var tResultName = typeof(TResult).Name;

        var handler = _serviceProvider.GetService<IRequestHandler<TRequest, TResult>>()
                ?? throw new InvalidOperationException($"Handler for {tRequestName} returning {tResultName} Not Found");

        var processors = _serviceProvider.GetServices<IRequestProcessor<TRequest, TResult>>();

        Func<Task<TResult>> invoke = () => handler.HandleAsync(request, cancellationToken);

        foreach (var processor in processors.Reverse())
        {
            var next = invoke;

            invoke = () => processor.ProcessAsync(request, next, cancellationToken);
        }

        var result = await invoke();

        return result;
    }
}
