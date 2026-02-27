using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace XMS.Web.Components.Layout.Sections
{
    public class SectionWarehouse : IAppSection
    {
        public string Key => "warehouse";
        public string Title => "Склад";

        public string Icon => Icons.Material.Filled.Warehouse;

        public RenderFragment Menu => builder =>
        {
            builder.OpenComponent(0, componentType: typeof(NavMenuWarehouse));
            builder.CloseComponent();
        };
    }
}
