using XMS.Core.Abstractions;

namespace XMS.Modules.Costs.Domain
{
    public class CostItem : NamedEntity
    {
        public ICollection<CostCategory>? Categories { get; set; } = [];

        public ICollection<CostCategoryItem>? CategoryItems { get; set; } = [];
    }
}
