namespace XMS.Application.Abstractions.Integration.OneC.Events;

public interface IIntegrationEventHandler
{
    Task HandleAsync(IOdataEntity integrationEvent, CancellationToken ct);
}

public interface IIntegrationEventHandler<in TEvent> : IIntegrationEventHandler
    where TEvent : class, IOdataEntity
{
    Task HandleAsync(TEvent eventValue, CancellationToken ct);

    Task IIntegrationEventHandler.HandleAsync(IOdataEntity integrationEvent, CancellationToken ct)
    {
        if (integrationEvent is TEvent typedEvent)
        {
            return HandleAsync(typedEvent, ct);
        }

        throw new ArgumentException($"Event type {integrationEvent.GetType().Name} does not match handler event type {typeof(TEvent).Name}");
    }
}
