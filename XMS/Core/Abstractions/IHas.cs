using System.ComponentModel.DataAnnotations;
using XMS.Modules.Employees.Domain;

namespace XMS.Core.Abstractions
{
    public interface IHasId<out TId>
    {
        TId Id { get; }
    }

    public interface IHasName
    {
        string Name { get; set; }
    }

    public interface IHasParent<TParent>
    {
        Guid? ParentId { get; set; }
        TParent? Parent { get; set; }
    }

    public interface IHasChildren<TDescendant>
    {
        ICollection<TDescendant> Children { get; set; }
    }
}
