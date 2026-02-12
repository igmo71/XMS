namespace XMS.Web.Core.Abstractions
{
    public interface IServiceResult
    { }

    public interface IServiceResult<TValue> : IServiceResult
    {
        TValue? Value { get; }
    }
}
