using XMS.Domain.Abstractions;

namespace XMS.Modules.CostModule.Domain
{
    /// <summary>
    /// Статьи Движения Денежных Средств к Статьям Затрат
    /// </summary>
    public class CostCatalog_СтатьиДвиженияДенежныхСредств : BaseEntity
    {
        /// <summary>
        /// Статья Движения Денежных Средств - Id
        /// </summary>
        public Guid CatalogRefKey { get; set; }
        /// <summary>
        /// Статья Движения Денежных Средств
        /// </summary>
        public CostCatalog_СтатьиДвиженияДенежныхСредств? CashFlowItem { get; set; }

        /// <summary>
        /// Категория и Статья Затрат - Id
        /// </summary>
        public  Guid CostCategoryItemId { get; set; }
        /// <summary>
        /// Категория и Статья Затрат
        /// </summary>
        public CostCategoryItem? CostCategoryItem { get; set; }
    }
}
