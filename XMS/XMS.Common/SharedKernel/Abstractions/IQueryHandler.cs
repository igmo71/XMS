namespace XMS.Common.SharedKernel.Abstractions;

public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
    where TResult : IResult
{ }
