using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using XMS.Integration.OneC.Common;
using Entity = XMS.Integration.OneC.Ut.Features.Catalog_КонтактныеЛицаПартнеров_Feature.Catalog_КонтактныеЛицаПартнеров;
using Handler = XMS.Integration.OneC.Ut.Abstractions.ICatalog_КонтактныеЛицаПартнеров_NotificationHandler;

namespace XMS.Integration.OneC.Ut.Features.Catalog_КонтактныеЛицаПартнеров_Feature;

internal class Catalog_КонтактныеЛицаПартнеров_NotificationConsumer(
    IConnectionFactory factory,
    IServiceProvider serviceProvider,
    IHostEnvironment hostEnvironment,
    ILogger<Catalog_КонтактныеЛицаПартнеров_NotificationConsumer> logger)
    : CatalogNotificationConsumer<Entity, CatalogNotification, Handler>(factory, serviceProvider, hostEnvironment, logger)
{ }
