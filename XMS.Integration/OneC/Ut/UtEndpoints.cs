using Microsoft.AspNetCore.Routing;
using XMS.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature;
using XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;

namespace XMS.Integration.OneC.Ut
{
    public static class UtEndpoints
    {
        public static IEndpointRouteBuilder MapUtEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapDocument_Catalog_Партнеры_Endpoints();
            builder.MapDocument_СписаниеБезналичныхДенежныхСредств_Endpoints();

            return builder;
        }
    }
}
