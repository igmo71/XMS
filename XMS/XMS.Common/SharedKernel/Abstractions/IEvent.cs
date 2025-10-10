namespace XMS.Common.SharedKernel.Abstractions;

public interface IEvent
{ }

public interface IEvent<TValue> : IEvent
{
    public TValue? Value { get; }
}
