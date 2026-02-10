using XMS.Core.Abstractions;

namespace XMS.Modules.Employees.Domain
{
    public class UserUt : BaseEntity, IHasName
    {
        public string Name { get; set; } = string.Empty;
        public bool DeletionMark { get; set; }
    }
}
