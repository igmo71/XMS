using XMS.Web.Core.Abstractions;
using XMS.Web.Modules.Departments.Domain;
using XMS.Web.Modules.Employees.Domain;

namespace XMS.Web.Modules.Costs.Domain
{
    public class CostCategory : BaseEntity, IHasName, ITreeNode<CostCategory>, ISoftDeletable
    {
        public string Name { get; set; } = string.Empty;

        public Guid? ParentId { get; set; }
        public CostCategory? Parent { get; set; }
        public ICollection<CostCategory> Children { get; set; } = [];

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }

        public ICollection<CostItem>? Items { get; set; } = [];

        public ICollection<CostCategoryItem>? CategoryItems { get; set; } = [];

        public Guid? DepartmentId { get; set; }
        public Department? Department { get; set; }

        public Guid? EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public CostCategory ClearCollections()
        {
            Items = null;
            CategoryItems = null;

            return this;
        }
    }
}
