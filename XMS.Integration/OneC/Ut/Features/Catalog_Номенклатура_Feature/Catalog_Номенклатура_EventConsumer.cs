using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using XMS.Integration.OneC.Common;
using Entity = XMS.Integration.OneC.Ut.Features.Catalog_Номенклатура_Feature.Catalog_Номенклатура;
using Handler = XMS.Integration.OneC.Ut.Abstractions.ICatalog_Номенклатура_EventHandler;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Номенклатура_Feature;

internal class Catalog_Номенклатура_EventConsumer(
    IConnectionFactory factory,
    IServiceProvider serviceProvider,
    IHostEnvironment hostEnvironment,
    ILogger<Catalog_Номенклатура_EventConsumer> logger)
    : CatalogEventConsumer<Entity, CatalogEvent, Handler>(factory, serviceProvider, hostEnvironment, logger)
{ }
