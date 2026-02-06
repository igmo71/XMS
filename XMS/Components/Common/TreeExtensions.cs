using MudBlazor;

namespace XMS.Components.Common
{
    public static class TreeExtensions
    {
        public static void SetExpansion<T>(this IEnumerable<ITreeItemData<T>>? items, bool expanded)
        {
            if (items == null) return;

            foreach (var item in items)
            {
                if (item is TreeItemData<T> node)
                {
                    node.Expanded = expanded;
                    node.Children?.SetExpansion(expanded);
                }
            }
        }   
    }
}
