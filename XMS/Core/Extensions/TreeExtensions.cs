using XMS.Core.Abstractions;

namespace XMS.Core.Extensions
{
    public static class TreeExtensions
    {
        public static IEnumerable<T> GetAllChildren<T>(this T parent)
            where T : IHasParent<T>, IHasChildren<T>
        {
            foreach (var child in parent.Children)
            {
                yield return child;
                foreach (var descendant in child.GetAllChildren())
                {
                    yield return descendant;
                }
            }
        }
    }
}
