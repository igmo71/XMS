using System.ComponentModel.DataAnnotations;
using XMS.SharedKernel.Abstractions;

namespace XMS.Modules.Employees.Domain
{
    public class Employee : Entity, IHasName
    {
        public string Name { get; set; } = string.Empty;

        [MaxLength(36)] public string? PostId { get; set; }
        public Post? Post { get; set; }

        //[MaxLength(36)] public string? CostItemId { get; set; }
        //public CostItem? CostItem { get; set; }

        //[MaxLength(36)] public string? DepartmentId { get; set; }
        //public Department? Department { get; set; }

        //[MaxLength(36)] public string? CityId { get; set; }
        //public City? City { get; set; }

        //[MaxLength(36)] public string? LocationId { get; set; }
        //public Location? Location { get; set; }

        //[MaxLength(36)] public string? UserUtId { get; set; }
        //[NotMapped]
        //public UserUt? UserUt { get; set; }

        //[MaxLength(AppSettings.SID)] public string? UserAdId { get; set; }
        //public AdUser? UserAd { get; set; }

        //[MaxLength(36)] public string? EmploeeBuhId { get; set; }
        //public EmploeeBuh? EmploeeBuh { get; set; }

        //[MaxLength(36)] public string? EmploeeZupId { get; set; }
        //public EmploeeZup? EmploeeZup { get; set; }

        //[MaxLength(36)] public Guid? OperationManagerId { get; set; }
        //public Employee? OperationManager { get; set; }

        //[MaxLength(36)] public Guid? LocationManagerId { get; set; }
        //public Employee? LocationManager { get; set; }
    }
}
