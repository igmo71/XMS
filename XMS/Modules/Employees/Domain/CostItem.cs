using XMS.Core.Abstractions;

namespace XMS.Modules.Employees.Domain
{
    public class CostItem : NamedEntity, ITreeNode<CostItem>
    {
        public Guid? ParentId { get; set; }
        public CostItem? Parent { get; set; }
        public virtual ICollection<CostItem> Children { get; set; } = [];
    }
}
