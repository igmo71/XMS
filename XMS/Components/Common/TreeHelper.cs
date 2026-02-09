using MudBlazor;
using XMS.Core.Abstractions;

namespace XMS.Components.Common
{
    public class TreeHelper
    {
        public static List<TreeItemData<T>> BuildTree<T>(IEnumerable<T> allItems, Guid? parentId, HashSet<Guid>? _expandedIds = null) where T : NamedEntity, IHasParent<T>
        {
            var result = allItems
                .Where(e => e.ParentId == parentId)
                .Select(e => new TreeItemData<T>
                {
                    Value = e,
                    Text = e.Name,
                    Expanded = _expandedIds is not null && _expandedIds.Contains(e.Id),
                    Children = BuildTree(allItems, e.Id, _expandedIds)
                })
                .ToList();

            return result;
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

        //public static HashSet<Guid> GetForbiddenParentIds(CostCategory currentCategory, IEnumerable<CostCategory> allCategories)
        //{
        //    var forbiddenIds = new HashSet<Guid>();
        //    if (currentCategory.Id == Guid.Empty) return forbiddenIds;

        //    forbiddenIds.Add(currentCategory.Id);

        //    // Собираем всех потомков рекурсивно
        //    var lookup = allCategories.ToLookup(x => x.ParentId);
        //    AddChildrenToForbiddenList(currentCategory.Id, lookup, forbiddenIds);

        //    return forbiddenIds;
        //}

        public static HashSet<Guid> GetForbiddenParentIds<T>(T current, IEnumerable<T> all) where T : BaseEntity, ITreeNode<T>
        {
            var forbiddenIds = new HashSet<Guid>();

            if (current.Id == Guid.Empty) return forbiddenIds;

            forbiddenIds.Add(current.Id);

            var lookup = all.ToLookup(x => x.ParentId);

            AddChildrenToForbiddenList(current.Id, lookup, forbiddenIds);

            return forbiddenIds;
        }


        private static void AddChildrenToForbiddenList<T>(Guid parentId, ILookup<Guid?, T> lookup, HashSet<Guid> forbiddenIds) where T : BaseEntity, ITreeNode<T>
        {
            foreach (var child in lookup[parentId])
            {
                forbiddenIds.Add(child.Id);
                AddChildrenToForbiddenList(child.Id, lookup, forbiddenIds);
            }
        }
    }
}
