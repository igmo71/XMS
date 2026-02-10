using XMS.Core.Abstractions;

namespace XMS.Modules.Employees.Domain
{
    public class EmployeeBuh : BaseEntity, IHasName
    {
        public string Name { get; set; } = string.Empty;
        public bool DeletionMark { get; set; }
        public string? Code { get; set; }
        public bool Archived { get; set; }
    }
}
