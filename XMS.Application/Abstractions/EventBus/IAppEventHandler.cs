namespace XMS.Application.Abstractions.EventBus;

public interface IAppEventHandler<in TEvent> where TEvent : class, IAppEvent
{
    Task HandleAsync(TEvent eventValue, CancellationToken ct = default);
}
