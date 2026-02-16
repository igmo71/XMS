using XMS.Domain.Abstractions;

namespace XMS.Domain.Models
{
    public class CashFlowItem : BaseEntity, IHasName, ISoftDeletable//, ITreeNode<CashFlowItem>
    {
        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; }
        public bool IsFolder { get; set; }

        public Guid? ParentId { get; set; }
        //public CashFlowItem? Parent { get; set; }

        //public virtual ICollection<CashFlowItem> Children { get; set; } = [];

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
