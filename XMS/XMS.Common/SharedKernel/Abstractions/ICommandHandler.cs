namespace XMS.Common.SharedKernel.Abstractions;

public interface ICommandHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
    where TResult : IResult
{ }
