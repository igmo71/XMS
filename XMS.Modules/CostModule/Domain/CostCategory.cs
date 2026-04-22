using XMS.Domain.Abstractions;
using XMS.Domain.Models;

namespace XMS.Modules.CostModule.Domain;

/// <summary>
/// Категория Статей Затрат
/// </summary>
public class CostCategory : BaseEntity, IHasName, ITreeNode<CostCategory>, ISoftDeletable
{
    public string Name { get; set; } = string.Empty;

    public Guid? ParentId { get; set; }
    public CostCategory? Parent { get; set; }
    public ICollection<CostCategory> Children { get; set; } = [];

    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    public ICollection<CostItem>? Items { get; set; } = [];

    public ICollection<CostCategoryItem>? CategoryItems { get; set; } = [];

    public Guid? DepartmentId { get; set; }
    public Department? Department { get; set; }

    public Guid? ManagerId { get; set; }
    public Employee? Manager { get; set; }

    public CostCategory ClearCollections()
    {
        Items = null;
        CategoryItems = null;

        return this;
    }
}
