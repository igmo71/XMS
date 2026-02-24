using Microsoft.AspNetCore.Components;

namespace XMS.Web.Components.Layout.Sections
{
    public interface IAppSection
    {
        string Key { get; }
        string Title { get; }
        RenderFragment Menu { get; }
    }
}
