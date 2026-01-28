using XMS.Core.Abstractions;

namespace XMS.Modules.Employees.Domain
{
    public class EmployeeZup : NamedEntity
    {
        public bool DeletionMark { get; set; }
        public string? Code { get; set; }
        public bool Archived { get; set; }
    }
}