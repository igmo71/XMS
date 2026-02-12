using XMS.Web.Core.Abstractions;

namespace XMS.Web.Modules.Costs.Domain
{
    public class CostItem : BaseEntity, IHasName, ISoftDeletable
    {
        public string Name { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }

        public ICollection<CostCategory>? Categories { get; set; } = [];

        public ICollection<CostCategoryItem>? CategoryItems { get; set; } = [];
    }
}
