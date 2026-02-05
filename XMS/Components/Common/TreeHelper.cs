using MudBlazor;
using XMS.Core.Abstractions;
using XMS.Modules.Costs.Domain;

namespace XMS.Components.Common
{
    public class TreeHelper
    {
        public static List<TreeItemData<T>> BuildTree<T>(IEnumerable<T> allItems, Guid? parentId) where T : NamedEntity, IHasParent<T>
        {
            return allItems
                .Where(d => d.ParentId == parentId)
                .Select(d => new TreeItemData<T>
                {
                    Value = d,
                    Text = d.Name,
                    Expanded = false,
                    Icon = Icons.Material.Filled.Category,
                    Children = BuildTree(allItems, d.Id)
                })                
                .ToList();
        }

        public static List<T> BuildFlattenedTree<T>(IEnumerable<T> items) where T : BaseEntity, IHasParent<T>, IHasName, new()
        {
            var result = new List<T>();

            var lookup = items.ToLookup(x => x.ParentId);

            void Build(Guid? parentKey, int level)
            {
                var children = lookup[parentKey].OrderBy(e => e.Name);
                foreach (var item in children)
                {
                    string indent = new('\u00A0', level * 4);
                    string prefix = level > 0 ? "└─ " : "";

                    result.Add(new T
                    {
                        Id = item.Id,
                        //Name = $"{new string('-', level)} {item.Name ?? string.Empty}"
                        Name = $"{indent}{prefix}{item.Name}"
                    });

                    Build(item.Id, level + 1);
                }
            }

            Build(null, 0);

            return result;
        }

        public static HashSet<Guid> GetForbiddenParentIds(CostCategory currentCategory, IEnumerable<CostCategory> allCategories)
        {
            var forbiddenIds = new HashSet<Guid>();
            if (currentCategory.Id == Guid.Empty) return forbiddenIds;

            forbiddenIds.Add(currentCategory.Id);

            // Собираем всех потомков рекурсивно
            var lookup = allCategories.ToLookup(x => x.ParentId);
            AddChildrenToForbiddenList(currentCategory.Id, lookup, forbiddenIds);

            return forbiddenIds;
        }

        private static void AddChildrenToForbiddenList(Guid parentId, ILookup<Guid?, CostCategory> lookup, HashSet<Guid> forbiddenIds)
        {
            foreach (var child in lookup[parentId])
            {
                forbiddenIds.Add(child.Id);
                AddChildrenToForbiddenList(child.Id, lookup, forbiddenIds);
            }
        }
    }
}
