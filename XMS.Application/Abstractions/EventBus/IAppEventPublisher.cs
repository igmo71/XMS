namespace XMS.Application.Abstractions.EventBus;

public interface IAppEventPublisher
{
    Task PublishAsync<TEvent>(TEvent eventValue, CancellationToken ct = default) where TEvent : class, IAppEvent;
}
