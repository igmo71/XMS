using XMS.Core.Abstractions;

namespace XMS.Modules.Departments.Domain
{
    public class Department : NamedEntity, ITreeNode<Department>, ISoftDelete
    {
        public Guid? ParentId { get; set; }
        public Department? Parent { get; set; }
        public virtual ICollection<Department> Children { get; set; } = [];

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }
    }
}
