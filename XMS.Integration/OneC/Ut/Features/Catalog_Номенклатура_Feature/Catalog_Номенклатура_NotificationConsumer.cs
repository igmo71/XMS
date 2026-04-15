using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using XMS.Integration.OneC.Common;
using Entity = XMS.Integration.OneC.Ut.Features.Catalog_Номенклатура_Feature.Catalog_Номенклатура;
using Handler = XMS.Integration.OneC.Ut.Abstractions.ICatalog_Номенклатура_NotificationHandler;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Номенклатура_Feature;

internal class Catalog_Номенклатура_NotificationConsumer(
    IConnectionFactory factory,
    IServiceProvider serviceProvider,
    IHostEnvironment hostEnvironment,
    ILogger<Catalog_Номенклатура_NotificationConsumer> logger)
    : CatalogNotificationConsumer<Entity, CatalogNotification, Handler>(factory, serviceProvider, hostEnvironment, logger)
{ }
