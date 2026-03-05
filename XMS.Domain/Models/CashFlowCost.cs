using XMS.Domain.Abstractions;

namespace XMS.Domain.Models
{
    /// <summary>
    /// Статьи Движения Денежных Средств к Статьям Затрат
    /// </summary>
    public class CashFlowCost : BaseEntity
    {
        /// <summary>
        /// Статья Движения Денежных Средств - Id
        /// </summary>
        public Guid CashFlowItemId { get; set; }
        public CashFlowItem? CashFlowItem { get; set; }

        /// <summary>
        /// Категория и Статья Затрат - Id
        /// </summary>
        public  Guid CostCategoryItemId { get; set; }
        public CostCategoryItem? CostCategoryItem { get; set; }
    }
}
