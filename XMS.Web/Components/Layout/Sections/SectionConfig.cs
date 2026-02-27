using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace XMS.Web.Components.Layout.Sections
{
    public class SectionConfig : IAppSection
    {
        public string Key => "config";
        public string Title => "Конфигурация";
        public string Icon => Icons.Material.Filled.SettingsApplications;

        public RenderFragment Menu => builder =>
        {
            builder.OpenComponent(0, componentType: typeof(NavMenuConfig));
            builder.CloseComponent();
        };
    }
}
