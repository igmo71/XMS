using XMS.Core.Abstractions.EventBus;

namespace XMS.Integration.Abstractions;

public interface IIntegrationEventHandler<in TEvent> : IEventHandler<TEvent>
    where TEvent : class, IIntegrationEvent
{ }
