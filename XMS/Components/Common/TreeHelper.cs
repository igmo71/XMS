using MudBlazor;
using XMS.Core.Abstractions;

namespace XMS.Components.Common
{
    public class TreeHelper
    {
        public static List<TreeItemData<T>> BuildTree<T>(IEnumerable<T> allDeps, Guid? parentId) where T : NamedEntity, IHasParent<T>
        {
            return allDeps
                .Where(d => d.ParentId == parentId)
                .Select(d => new TreeItemData<T>
                {
                    Value = d,
                    Text = d.Name,
                    Expanded = false, // Начальное состояние
                    Icon = Icons.Material.Filled.Category,
                    // Рекурсивно загружаем детей
                    Children = BuildTree(allDeps, d.Id)
                })
                .ToList();
        }
    }
}
