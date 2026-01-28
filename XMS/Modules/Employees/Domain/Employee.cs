using XMS.Core.Abstractions;

namespace XMS.Modules.Employees.Domain
{
    public class Employee : NamedEntity
    {
        public Guid? PostId { get; set; }
        public Post? Post { get; set; }

        public Guid? DepartmentId { get; set; }
        public Department? Department { get; set; }

        public Guid? CityId { get; set; }
        public City? City { get; set; }

        public Guid? LocationId { get; set; }
        public Location? Location { get; set; }

        public Guid? CostItemId { get; set; }
        public CostItem? CostItem { get; set; }

        public string? UserAdId { get; set; }
        public UserAd? UserAd { get; set; }

        public Guid? UserUtId { get; set; }
        public UserUt? UserUt { get; set; }

        public Guid? EmployeeBuhId { get; set; }
        public EmployeeBuh? EmployeeBuh { get; set; }

        public Guid? EmployeeZupId { get; set; }
        public EmployeeZup? EmployeeZup { get; set; }

        //public Guid? OperationManagerId { get; set; }
        //public Employee? OperationManager { get; set; }

        //public Guid? LocationManagerId { get; set; }
        //public Employee? LocationManager { get; set; }
    }
}
