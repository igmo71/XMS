namespace XMS.Common.SharedKernel.Abstractions;

public interface IEntity<TId>
{
    public TId Id { get; }
}
