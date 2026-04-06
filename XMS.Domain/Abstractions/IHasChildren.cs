namespace XMS.Domain.Abstractions;

public interface IHasChildren<TDescendant>
{
    ICollection<TDescendant> Children { get; set; }
}
