namespace XMS.Domain.Models
{
    /// <summary>
    /// Статьи Затпрат по Категориям
    /// </summary>
    public class CostCategoryItem
    {
        public Guid CategoryId { get; set; }
        public CostCategory? Category { get; set; }

        public Guid ItemId { get; set; }
        public CostItem? Item { get; set; }
    }
}
