using XMS.Core.Abstractions;

namespace XMS.Modules.Costs.Domain
{
    public class CostCategory : NamedEntity, ITreeNode<CostCategory>
    {
        public Guid? ParentId { get; set; }

        public CostCategory? Parent { get; set; }

        public ICollection<CostCategory> Children { get; set; } = [];

        public ICollection<CostItem>? Items { get; set; } = [];

        public ICollection<CostCategoryItem>? CategoryItems { get; set; } = [];
    }
}
