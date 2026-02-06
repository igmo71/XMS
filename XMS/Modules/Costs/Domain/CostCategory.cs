using XMS.Core.Abstractions;
using XMS.Modules.Departments.Domain;
using XMS.Modules.Employees.Domain;

namespace XMS.Modules.Costs.Domain
{
    public class CostCategory : NamedEntity, ITreeNode<CostCategory>
    {
        private Department? _department;
        private Employee? _employee;

        public Guid? ParentId { get; set; }

        public CostCategory? Parent { get; set; }

        public ICollection<CostCategory> Children { get; set; } = [];

        public ICollection<CostItem>? Items { get; set; } = [];

        public ICollection<CostCategoryItem>? CategoryItems { get; set; } = [];

        public Guid? DepartmentId { get; set; }
        public Department? Department
        {
            get => _department;
            set
            {
                _department = value;
                DepartmentId = value?.Id;
            }
        }

        public Guid? EmployeeId { get; set; }
        public Employee? Employee
        {
            get => _employee;
            set
            {
                _employee = value;
                EmployeeId = value?.Id;
            }
        }

        public CostCategory ClearNavigationProperties()
        {
            _department = null;
            _employee = null;

            return this;
        }

        public CostCategory ClearCollections()
        {
            Items = null;
            CategoryItems = null;

            return this;
        }
    }
}
