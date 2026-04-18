namespace XMS.EventBus.Abstractions;

public interface IAppEventPublisher
{
    Task PublishAsync<TEvent>(TEvent eventValue, CancellationToken ct = default) where TEvent : class, IAppEvent;
}
