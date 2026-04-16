namespace XMS.Core.Abstractions.EventBus;

public interface IEventHandler<in TEvent> where TEvent : IEvent
{
    Task HandleAsync(TEvent eventValue, CancellationToken ct = default);
}
