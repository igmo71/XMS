using XMS.Domain.Abstractions;

namespace XMS.Domain.Models
{
    public class Location : BaseEntity, IHasName, ISoftDeletable
    {
        public string Name { get; set; } = string.Empty;

        public Guid? ManagerId { get; set; }
        public Employee? Manager { get; set; }

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
