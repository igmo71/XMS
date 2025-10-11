namespace XMS.Common.SharedKernel.Abstractions;

public interface IHasId<TId>
{
    TId Id { get; }
}
