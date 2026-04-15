namespace XMS.Core.Abstractions.EventBus;

public interface IEventHandler<in TEvent>
{
    Task HandleAsync(TEvent eventValue, CancellationToken ct = default);
}
