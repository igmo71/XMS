namespace XMS.Core.Abstractions.EventBus;

public interface IEventPublisher
{
    Task PublishAsync<TEvent>(string exchange, TEvent eventValue, CancellationToken ct = default);
}
