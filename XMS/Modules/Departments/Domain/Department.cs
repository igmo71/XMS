using XMS.Core.Abstractions;

namespace XMS.Modules.Departments.Domain
{
    public class Department : NamedEntity, ITreeNode<Department>
    {
        public Guid? ParentId { get; set; }
        public Department? Parent { get; set; }
        public virtual ICollection<Department> Children { get; set; } = [];
    }
}
