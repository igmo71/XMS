using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using XMS.Integration.OneC.Common;
using Entity = XMS.Integration.OneC.Ut.Features.Catalog_Пользователи_Feature.Catalog_Пользователи;
using Handler = XMS.Integration.OneC.Ut.Abstractions.ICatalog_Пользователи_NotificationHandler;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Пользователи_Feature;

internal class Catalog_Пользователи_NotificationConsumer(
    IConnectionFactory factory,
    IServiceProvider serviceProvider,
    IHostEnvironment hostEnvironment,
    ILogger<Catalog_Пользователи_NotificationConsumer> logger)
    : CatalogNotificationConsumer<Entity, CatalogNotification, Handler>(factory, serviceProvider, hostEnvironment, logger)
{ }
