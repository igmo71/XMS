using Microsoft.AspNetCore.Components;

namespace XMS.Web.Components.Layout.Sections
{
    public class SectionConfig : IAppSection
    {
        public string Key => "config";
        public string Title => "Конфигурация";

        public RenderFragment Menu => builder =>
        {
            builder.OpenComponent(0, componentType: typeof(NavMenuConfig));
            builder.CloseComponent();
        };
    }
}
