namespace XMS.EventBus.Abstractions;

public interface IEventNamingService
{
    string GetEventName(Type eventType);
    string GetEventName<T>() => GetEventName(typeof(T));

    string DeadLetterName { get; }
}
