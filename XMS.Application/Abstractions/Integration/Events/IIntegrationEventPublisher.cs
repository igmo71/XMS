namespace XMS.Application.Abstractions.Integration.Events;

public interface IIntegrationEventPublisher //: IAppEventPublisher
{
    Task PublishAsync<TEvent>(TEvent eventValue, CancellationToken ct = default) where TEvent : class, IIntegrationEvent;
}
