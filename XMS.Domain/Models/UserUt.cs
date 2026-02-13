using XMS.Domain.Abstractions;

namespace XMS.Domain.Models
{
    public class UserUt : BaseEntity, IHasName
    {
        public string Name { get; set; } = string.Empty;
        public bool DeletionMark { get; set; }
    }
}
