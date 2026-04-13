using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using XMS.Integration.OneC.Common;
using XMS.Integration.OneC.Ut.Features.Catalog_Номенклатура_Feature;
using Entity = XMS.Integration.OneC.Ut.Features.Catalog_КСЗ_КатегорииСтатейЗатрат_Feature.Catalog_КСЗ_КатегорииСтатейЗатрат;
using Handler = XMS.Integration.OneC.Ut.Abstractions.ICatalog_КСЗ_КатегорииСтатейЗатрат_EventHandler;

namespace XMS.Integration.OneC.Ut.Features.Catalog_КСЗ_КатегорииСтатейЗатрат_Feature;

internal class Catalog_КСЗ_КатегорииСтатейЗатрат_EventConsumer(
    IConnectionFactory factory,
    IServiceProvider serviceProvider,
    IHostEnvironment hostEnvironment,
    ILogger<Catalog_Номенклатура_EventConsumer> logger)
    : CatalogEventConsumer<Entity, CatalogEvent, Handler>(factory, serviceProvider, hostEnvironment, logger)
{ }
