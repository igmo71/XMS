namespace XMS.Web.Core.Abstractions
{
    public interface ITreeNode<T> : IHasParent<T>, IHasChildren<T>
   where T : class
    {
    }
}
