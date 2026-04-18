namespace XMS.Integration.Abstractions;

public interface IIntegrationEventPublisher //: IAppEventPublisher
{
    Task PublishAsync<TEvent>(TEvent eventValue, CancellationToken ct = default) where TEvent : class, IIntegrationEvent;
}
