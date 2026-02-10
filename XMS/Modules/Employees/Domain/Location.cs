using XMS.Core.Abstractions;

namespace XMS.Modules.Employees.Domain
{
    public class Location : BaseEntity, IHasName
    {
        public string Name { get; set; } = string.Empty;
    }
}
