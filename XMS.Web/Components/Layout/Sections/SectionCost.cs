using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace XMS.Web.Components.Layout.Sections
{
    public class SectionCost : IAppSection
    {
        public string Key => "cost";
        public string Title => "Затраты";
        public string Icon => Icons.Material.Filled.CurrencyRuble;

        public RenderFragment Menu => builder =>
        {
            builder.OpenComponent(0, componentType: typeof(NavMenuCost));
            builder.CloseComponent();
        };
    }
}
