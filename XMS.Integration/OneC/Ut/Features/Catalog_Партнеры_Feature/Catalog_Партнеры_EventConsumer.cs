using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Entity = XMS.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature.Catalog_Партнеры;
using Handler = XMS.Integration.OneC.Ut.Abstractions.ICatalog_Партнеры_EventHandler;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature;

internal class Catalog_Партнеры_EventConsumer(
    IConnectionFactory factory,
    IServiceProvider serviceProvider,
    IHostEnvironment hostEnvironment,
    ILogger<Catalog_Партнеры_EventConsumer> logger)
    : CatalogEventConsumer<Entity, CatalogEvent, Handler>(factory, serviceProvider, hostEnvironment, logger)
{ }