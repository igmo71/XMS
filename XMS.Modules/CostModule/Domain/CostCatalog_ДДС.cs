using System.ComponentModel.DataAnnotations.Schema;
using XMS.Application.Integration.OneC.Ut.Features.Catalog_СтатьиДвиженияДенежныхСредств_Feature;
using XMS.Domain.Abstractions;

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
    public Guid Catalog_СтатьяДДС_Key { get; set; }

    /// <summary>
    /// Статья Движения Денежных Средств
    /// </summary>
    [NotMapped]
    public Catalog_СтатьиДвиженияДенежныхСредств? Catalog_СтатьяДДС { get; set; }
}
