namespace XMS.Application.Abstractions
{
    public interface IServiceResult
    { }

    public interface IServiceResult<TValue> : IServiceResult
    {
        TValue? Value { get; }
    }
}
