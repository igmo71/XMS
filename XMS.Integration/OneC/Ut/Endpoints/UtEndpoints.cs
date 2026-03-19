using Microsoft.AspNetCore.Routing;

namespace XMS.Integration.OneC.Ut.Endpoints
{
    public static class UtEndpoints
    {
        public static IEndpointRouteBuilder MapUtEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapDocument_СписаниеБезналичныхДенежныхСредств_Endpoints();

            return builder;
        }
    }
}
