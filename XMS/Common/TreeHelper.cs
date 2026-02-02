using XMS.Core.Abstractions;

namespace XMS.Common
{
    public class TreeHelper
    {
        public static List<T> BuildFlattenedTree<T>(IReadOnlyList<T> items) where T : BaseEntity, IHasParent<T>, IHasName, new()
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
    }
}
