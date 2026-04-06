namespace XMS.Domain.Abstractions;

public interface IHasParent<TParent>
{
    Guid? ParentId { get; set; }
    TParent? Parent { get; set; }
}
