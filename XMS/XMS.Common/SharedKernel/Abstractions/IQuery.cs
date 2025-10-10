namespace XMS.Common.SharedKernel.Abstractions;

public interface IQuery<TResult> : IRequest<TResult>
    where TResult : IResult
{ }
