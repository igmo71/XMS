using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using XMS.Integration.OneC.Common;
using Entity = XMS.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature.Catalog_Партнеры;
using Handler = XMS.Integration.OneC.Ut.Abstractions.ICatalog_Партнеры_NotificationHandler;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature;

internal class Catalog_Партнеры_NotificationConsumer(
    IConnectionFactory factory,
    IServiceProvider serviceProvider,
    IHostEnvironment hostEnvironment,
    ILogger<Catalog_Партнеры_NotificationConsumer> logger)
    : CatalogNotificationConsumer<Entity, CatalogNotification, Handler>(factory, serviceProvider, hostEnvironment, logger)
{ }