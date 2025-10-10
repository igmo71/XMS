namespace XMS.Common.SharedKernel.Abstractions;

public interface ICommand<TResult> : IRequest<TResult>
    where TResult : IResult
{ }
