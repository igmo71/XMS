using XMS.Domain.Abstractions;

namespace XMS.Domain.Models
{
    public class EmployeeBuh : BaseEntity, IHasName, ISoftDeletable
    {
        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; }
        public bool Archived { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
