using System.Diagnostics.CodeAnalysis;
using XMS.Common.SharedKernel.Abstractions;

namespace XMS.Common.SharedKernel;

public class Result<TValue> : IResult<TValue>
{
    private Result(TValue value)
    {
        IsSuccess = true;
        Value = value;
        Error = null;
    }

    private Result(Error error)
    {
        IsSuccess = false;
        Value = default;
        Error = error;
    }

    [MemberNotNullWhen(true, nameof(Value))]
    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSuccess { get; }
    public TValue? Value { get; }
    public Error? Error { get; }

    // Helper methods for constructing the `Result<T>`
    public static Result<TValue> Success(TValue value) => new(value);
    public static Result<TValue> Fail(Error error) => new(error);

    // Allow converting a T directly into Result<T>
    public static implicit operator Result<TValue>(TValue value) => Success(value);
    public static implicit operator Result<TValue>(Error error) => Fail(error);
}
