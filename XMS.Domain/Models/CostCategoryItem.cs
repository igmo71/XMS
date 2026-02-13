namespace XMS.Domain.Models
{
    public class CostCategoryItem
    {
        public Guid CategoryId { get; set; }
        public CostCategory? Category { get; set; }

        public Guid ItemId { get; set; }
        public CostItem? Item { get; set; }
    }
}
