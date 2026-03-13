using XMS.Domain.Abstractions;

namespace XMS.Domain.Models
{
    public class Department : BaseEntity, IHasName, ITreeNode<Department>, ISoftDeletable
    {
        public string Name { get; set; } = string.Empty;

        public Guid? ManagerId { get; set; }
        public Employee? Manager { get; set; }

        public Guid? ParentId { get; set; }
        public Department? Parent { get; set; }

        public virtual ICollection<Department> Children { get; set; } = [];

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
