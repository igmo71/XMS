using XMS.Core.Abstractions;
using XMS.Modules.Departments.Domain;
using XMS.Modules.Employees.Domain;

namespace XMS.Modules.Costs.Domain
{
    public class CostCategory : BaseEntity, IHasName, ITreeNode<CostCategory>
    {
        public string Name { get; set; } = string.Empty;

        public Guid? ParentId { get; set; }

        public CostCategory? Parent { get; set; }

        public ICollection<CostCategory> Children { get; set; } = [];

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
