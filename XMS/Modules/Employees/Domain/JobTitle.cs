using System.ComponentModel.DataAnnotations;
using XMS.Core.Abstractions;

namespace XMS.Modules.Employees.Domain
{
    public class JobTitle : BaseEntity, IHasName
    {
        public string Name { get; set; } = string.Empty;
    }
}
