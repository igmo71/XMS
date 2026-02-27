using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace XMS.Web.Components.Layout.Sections
{
    public class SectionAdmin : IAppSection
    {
        public string Key => "admin";
        public string Title => "Админка";
        public string Icon => Icons.Material.Filled.Settings;

        public RenderFragment Menu => builder =>
        {
            builder.OpenComponent(0, componentType: typeof(NavMenuAdmin));
            builder.CloseComponent();
        };
    }
}
