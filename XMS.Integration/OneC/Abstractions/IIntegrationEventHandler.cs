using XMS.Core.Abstractions.EventBus;

namespace XMS.Integration.OneC.Abstractions;

public interface IIntegrationEventHandler<in TEvent> : IEventHandler<TEvent>
    where TEvent : class, IIntegrationEvent
{
    //Task<ServiceResult> HandleEvent(TEvent oneCNotifyMessage, CancellationToken ct = default);
}
