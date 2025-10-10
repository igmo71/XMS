namespace XMS.Common.SharedKernel.Abstractions;

public interface IResult
{ }

public interface IResult<TValue> : IResult
{
    TValue? Value { get; }
}
