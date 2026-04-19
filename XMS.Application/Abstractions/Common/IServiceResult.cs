namespace XMS.Application.Abstractions.Common;

public interface IServiceResult
{ }

public interface IServiceResult<TValue> : IServiceResult
{
    TValue? Value { get; }
}
