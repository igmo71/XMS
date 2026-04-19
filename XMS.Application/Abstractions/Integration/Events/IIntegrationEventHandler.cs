namespace XMS.Application.Abstractions.Integration.Events;

public interface IIntegrationEventHandler
{
    Task HandleAsync(IIntegrationEvent integrationEvent, CancellationToken ct);
}

public interface IIntegrationEventHandler<in TEvent> : IIntegrationEventHandler
    where TEvent : class, IIntegrationEvent
{
    Task HandleAsync(TEvent eventValue, CancellationToken ct);

    Task IIntegrationEventHandler.HandleAsync(IIntegrationEvent integrationEvent, CancellationToken ct)
    {
        if (integrationEvent is TEvent typedEvent)
        {
            return HandleAsync(typedEvent, ct);
        }

        throw new ArgumentException($"Event type {integrationEvent.GetType().Name} does not match handler event type {typeof(TEvent).Name}");
    }
}
