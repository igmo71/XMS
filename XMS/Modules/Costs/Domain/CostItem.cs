using XMS.Core.Abstractions;

namespace XMS.Modules.Costs.Domain
{
    public class CostItem : BaseEntity, IHasName
        {
            public string Name { get; set; } = string.Empty;

        public ICollection<CostCategory>? Categories { get; set; } = [];

        public ICollection<CostCategoryItem>? CategoryItems { get; set; } = [];
    }
}
