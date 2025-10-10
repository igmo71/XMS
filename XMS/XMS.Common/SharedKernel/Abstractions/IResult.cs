namespace XMS.Common.SharedKernel.Abstractions;

public interface IResult
{ }

public interface IResult<TValue> : IResult
{
    public TValue? Value { get; }
}
