using System.ComponentModel.DataAnnotations.Schema;
using XMS.Domain.Abstractions;
using XMS.Integration.OneC.Ut.Features.Catalog_СтатьиДвиженияДенежныхСредств_Feature;

namespace XMS.Modules.CostModule.Domain;

/// <summary>
/// Статьи Движения Денежных Средств к Статьям Затрат
/// </summary>
public class CostCatalog_ДДС : BaseEntity
{
    /// <summary>
    /// Категория и Статья Затрат - Id
    /// </summary>
    public Guid CostCategoryItemId { get; set; }
    /// <summary>
    /// Категория и Статья Затрат
    /// </summary>
    public CostCategoryItem? CostCategoryItem { get; set; }


    /// <summary>
    /// Статья Движения Денежных Средств - Ref_Key
    /// </summary>
    public Guid Catalog_СтатьиДвиженияДенежныхСредств_RefKey { get; set; }

    /// <summary>
    /// Статья Движения Денежных Средств
    /// </summary>
    [NotMapped]
    public Catalog_СтатьиДвиженияДенежныхСредств? Catalog_СтатьиДвиженияДенежныхСредств { get; set; }
}
