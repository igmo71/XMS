namespace XMS.Common.SharedKernel.Abstractions;

public interface IRequest
{ }

public interface IRequest<TResult> : IRequest
    where TResult : IResult
{ }
