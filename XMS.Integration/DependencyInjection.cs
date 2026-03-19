using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace XMS.Integration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIntegration(this IServiceCollection services, IConfiguration configuration)
        {


            return services;
        }
    }
}
