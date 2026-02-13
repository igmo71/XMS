using XMS.Domain.Abstractions;

namespace XMS.Domain.Models
{
    public class JobTitle : BaseEntity, IHasName, ISoftDeletable
    {
        public string Name { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
