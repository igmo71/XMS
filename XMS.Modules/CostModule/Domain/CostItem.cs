using XMS.Domain.Abstractions;

namespace XMS.Modules.CostModule.Domain;

/// <summary>
/// Статья Затрат
/// </summary>
public class CostItem : BaseEntity, IHasName, ISoftDeletable
{
    public string Name { get; set; } = string.Empty;

    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    public ICollection<CostCategory>? Categories { get; set; } = [];

    public ICollection<CostCategoryItem>? CategoryItems { get; set; } = [];
}
