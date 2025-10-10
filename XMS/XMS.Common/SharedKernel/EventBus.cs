using Microsoft.Extensions.DependencyInjection;
using XMS.Common.SharedKernel.Abstractions;

namespace XMS.Common.SharedKernel;

public class EventBus(IServiceProvider serviceProvider) : IEventBus
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : IEvent
    {
        var handlers = _serviceProvider.GetServices<IEventHandler<TEvent>>();

        var handlingTasks = handlers.Select(h => h.HandleAsync(@event, cancellationToken));

        await Task.WhenAll(handlingTasks);
    }
}
