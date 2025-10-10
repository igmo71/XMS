namespace XMS.Common.SharedKernel.Abstractions;

public interface IEvent
{ }

public interface IEvent<TValue> : IEvent
{
    TValue? Value { get; }
}
