using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Entity = XMS.Integration.OneC.Ut.Features.Catalog_СтатьиДвиженияДенежныхСредств_Feature.Catalog_СтатьиДвиженияДенежныхСредств;
using Handler = XMS.Integration.OneC.Ut.Abstractions.ICatalog_СтатьиДвиженияДенежныхСредств_EventHandler;

namespace XMS.Integration.OneC.Ut.Features.Catalog_СтатьиДвиженияДенежныхСредств_Feature;

internal class Catalog_СтатьиДвиженияДенежныхСредств_EventConsumer(
    IConnectionFactory factory,
    IServiceProvider serviceProvider,
    IHostEnvironment hostEnvironment,
    ILogger<Catalog_СтатьиДвиженияДенежныхСредств_EventConsumer> logger)
    : CatalogEventConsumer<Entity, CatalogEvent, Handler>(factory, serviceProvider, hostEnvironment, logger)
{ }
