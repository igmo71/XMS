using XMS.Domain.Abstractions;

namespace XMS.Domain.Models
{
    /// <summary>
    /// Статьи Затпрат по Категориям
    /// </summary>
    public class CostCategoryItem : BaseEntity
    {
        /// <summary>
        /// Категория Статей Затрат - Id
        /// </summary>
        public Guid CategoryId { get; set; }
        /// <summary>
        /// Статья Затрат
        /// </summary>
        public CostCategory? Category { get; set; }

        /// <summary>
        /// Статья Затрат - Id
        /// </summary>
        public Guid ItemId { get; set; }
        /// <summary>
        /// Статья Затрат
        /// </summary>
        public CostItem? Item { get; set; }        
    }
}
