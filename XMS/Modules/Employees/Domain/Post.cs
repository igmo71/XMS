using System.ComponentModel.DataAnnotations;
using XMS.SharedKernel.Abstractions;

namespace XMS.Modules.Employees.Domain
{
    public class Post : Entity, IHasName
    {
        [MaxLength(AppSettings.MaxLength.NAME)]
        public string Name { get; set; } = string.Empty;
    }
}
