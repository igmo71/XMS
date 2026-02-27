using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace XMS.Web.Components.Layout.Sections
{
    public interface IAppSection
    {
        string Key { get; }
        string Title { get; }
        string Icon { get; }
        RenderFragment Menu { get; }
    }
}
