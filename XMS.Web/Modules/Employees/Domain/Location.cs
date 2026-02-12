using XMS.Web.Core.Abstractions;

namespace XMS.Web.Modules.Employees.Domain
{
    public class Location : BaseEntity, IHasName, ISoftDeletable
    {
        public string Name { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }
    }
}
