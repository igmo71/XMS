using XMS.Web.Core.Abstractions;

namespace XMS.Web.Modules.Departments.Domain
{
    public class Department : BaseEntity, IHasName, ITreeNode<Department>, ISoftDeletable
    {
        public string Name { get; set; } = string.Empty;

        public Guid? ParentId { get; set; }
        public Department? Parent { get; set; }

        public virtual ICollection<Department> Children { get; set; } = [];

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }
    }
}
