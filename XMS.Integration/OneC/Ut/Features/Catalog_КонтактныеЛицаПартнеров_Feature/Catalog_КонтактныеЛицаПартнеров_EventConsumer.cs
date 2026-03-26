using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Entity = XMS.Integration.OneC.Ut.Features.Catalog_КонтактныеЛицаПартнеров_Feature.Catalog_КонтактныеЛицаПартнеров;
using Handler = XMS.Integration.OneC.Ut.Abstractions.ICatalog_КонтактныеЛицаПартнеров_EventHandler;

namespace XMS.Integration.OneC.Ut.Features.Catalog_КонтактныеЛицаПартнеров_Feature;

internal class Catalog_КонтактныеЛицаПартнеров_EventConsumer(
    IConnectionFactory factory,
    IServiceProvider serviceProvider,
    IHostEnvironment hostEnvironment,
    ILogger<Catalog_КонтактныеЛицаПартнеров_EventConsumer> logger)
    : CatalogEventConsumer<Entity, CatalogEvent, Handler>(factory, serviceProvider, hostEnvironment, logger)
{ }
