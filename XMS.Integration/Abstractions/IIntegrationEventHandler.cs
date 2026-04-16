namespace XMS.Integration.Abstractions;

public interface IIntegrationEventHandler
{
    Task HandleAsync(IIntegrationEvent integrationEvent, CancellationToken ct);
}

public interface IIntegrationEventHandler<in TEvent> : IIntegrationEventHandler
    where TEvent : class, IIntegrationEvent
{
    Task HandleAsync(TEvent eventValue, CancellationToken ct);

    // Явная реализация базового метода (Default Interface Member)
    // Перенаправляет вызов на типизированный метод
    Task IIntegrationEventHandler.HandleAsync(IIntegrationEvent eventValue, CancellationToken ct)
    {
        if (eventValue is TEvent typedEvent)
        {
            return HandleAsync(typedEvent, ct);
        }

        throw new ArgumentException($"Event type {eventValue.GetType().Name} does not match handler event type {typeof(TEvent).Name}");
    }
}
