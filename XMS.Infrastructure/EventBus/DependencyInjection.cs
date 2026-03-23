using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using XMS.Integration.OneC.Abstractions;
using XMS.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature;
using XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;

namespace XMS.Infrastructure.EventBus
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(config =>
            {
                //var nameFormatter = new KebabCaseEndpointNameFormatter("xms", false);
                //config.SetEndpointNameFormatter(nameFormatter);
                //config.AddConsumers(Assembly.GetExecutingAssembly());
                config.AddConsumers(typeof(IOneCConsumer<>).Assembly);
                //config.AddConsumer<Catalog_Партнеры_Consumer>();
                //config.AddConsumer<Document_СписаниеБезналичныхДенежныхСредств_Consumer>();

                config.UsingRabbitMq((context, cfg) =>
                {
                    var rabbitMqConfig = configuration.GetSection(nameof(RabbitMqConfig)).Get<RabbitMqConfig>()
                        ?? throw new InvalidOperationException("RabbitMqConfig Not Found");

                    cfg.Host(rabbitMqConfig.Host, "/", h =>
                    {
                        h.Username(rabbitMqConfig.Username);
                        h.Password(rabbitMqConfig.Password);

                    });
                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}
