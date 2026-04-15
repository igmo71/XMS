using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using XMS.Integration.OneC.Common;
using Entity = XMS.Integration.OneC.Ut.Features.Catalog_СтатьиДвиженияДенежныхСредств_Feature.Catalog_СтатьиДвиженияДенежныхСредств;
using Handler = XMS.Integration.OneC.Ut.Abstractions.ICatalog_СтатьиДвиженияДенежныхСредств_NotificationHandler;

namespace XMS.Integration.OneC.Ut.Features.Catalog_СтатьиДвиженияДенежныхСредств_Feature;

internal class Catalog_СтатьиДвиженияДенежныхСредств_NotificationConsumer(
    IConnectionFactory factory,
    IServiceProvider serviceProvider,
    IHostEnvironment hostEnvironment,
    ILogger<Catalog_СтатьиДвиженияДенежныхСредств_NotificationConsumer> logger)
    : CatalogNotificationConsumer<Entity, CatalogNotification, Handler>(factory, serviceProvider, hostEnvironment, logger)
{ }
