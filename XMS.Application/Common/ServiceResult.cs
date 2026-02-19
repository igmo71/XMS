using System.Diagnostics.CodeAnalysis;
using XMS.Application.Abstractions;

namespace XMS.Application.Common
{
    public class ServiceResult : IServiceResult
    {
        [MemberNotNullWhen(false, nameof(Error))]
        public bool IsSuccess { get; }
        public ServiceError? Error { get; }

        private ServiceResult()
        {
            IsSuccess = true;
            Error = null;
        }

        private ServiceResult(ServiceError error)
        {
            IsSuccess = false;
            Error = error;
        }

        // Helper methods for constructing the `Result<T>`
        public static ServiceResult Success() => new();
        public static ServiceResult Fail(ServiceError error) => new(error);

        // Allow converting a T directly into Result
        public static implicit operator ServiceResult(ServiceError error) => Fail(error);
    }

    public class ServiceResult<TValue> : IServiceResult<TValue>
    {
        [MemberNotNullWhen(true, nameof(Value))]
        [MemberNotNullWhen(false, nameof(Error))]
        public bool IsSuccess { get; }
        public TValue? Value { get; }
        public ServiceError? Error { get; }

        private ServiceResult(TValue value)
        {
            IsSuccess = true;
            Value = value;
            Error = null;
        }

        private ServiceResult(ServiceError error)
        {
            IsSuccess = false;
            Value = default;
            Error = error;
        }

        // Helper methods for constructing the `Result<T>`
        public static ServiceResult<TValue> Success(TValue value) => new(value);
        public static ServiceResult<TValue> Fail(ServiceError error) => new(error);

        // Allow converting a T directly into Result<T>
        public static implicit operator ServiceResult<TValue>(TValue value) => Success(value);
        public static implicit operator ServiceResult<TValue>(ServiceError error) => Fail(error);
    }
}
