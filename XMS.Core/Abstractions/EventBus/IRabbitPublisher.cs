namespace XMS.Core.Abstractions.EventBus;

public interface IRabbitPublisher
{
    Task PublishAsync<T>(string exchange, T message);
}
