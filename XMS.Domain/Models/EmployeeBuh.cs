using XMS.Domain.Abstractions;

namespace XMS.Domain.Models
{
    public class EmployeeBuh : BaseEntity, IHasName
    {
        public string Name { get; set; } = string.Empty;
        public bool DeletionMark { get; set; }
        public string? Code { get; set; }
        public bool Archived { get; set; }
    }
}
