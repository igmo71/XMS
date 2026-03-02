using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace XMS.Web.Components.Layout.Sections
{
    public class SectionApi : IAppSection
    {
        public string Key => "api";

        public string Title => "API";

        public string Icon => Icons.Material.Filled.Api;

        public RenderFragment Menu => builder =>
        {
            builder.OpenComponent(0, componentType: typeof(NavMenuApi));
            builder.CloseComponent();
        };
    }
}
