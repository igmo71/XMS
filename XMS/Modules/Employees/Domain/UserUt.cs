using XMS.Core.Abstractions;

namespace XMS.Modules.Employees.Domain
{
    public class UserUt : NamedEntity
    {
        public bool DeletionMark { get; set; }
    }
}
