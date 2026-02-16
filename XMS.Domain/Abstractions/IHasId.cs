namespace XMS.Domain.Abstractions
{
    public interface IHasId<out TId>
    {
        TId Id { get; }
    }      
}
