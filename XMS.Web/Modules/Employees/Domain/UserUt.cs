using XMS.Web.Core.Abstractions;

namespace XMS.Web.Modules.Employees.Domain
{
    public class UserUt : BaseEntity, IHasName
    {
        public string Name { get; set; } = string.Empty;
        public bool DeletionMark { get; set; }
    }
}
