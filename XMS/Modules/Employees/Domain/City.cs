using XMS.Core.Abstractions;

namespace XMS.Modules.Employees.Domain
{
    public class City : BaseEntity, IHasName
    {
        public string Name { get; set; } = string.Empty;
    }
}
