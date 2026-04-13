namespace XMS.Core.Abstractions.EventBus;

public interface IEventPublisher
{
    Task PublishAsync<T>(string exchange, T message);
}
