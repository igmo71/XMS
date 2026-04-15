using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using XMS.Integration.OneC.Common;
using XMS.Integration.OneC.Ut.Features.Catalog_Номенклатура_Feature;
using Entity = XMS.Integration.OneC.Ut.Features.Catalog_КСЗ_КатегорииЗатрат_Feature.Catalog_КСЗ_КатегорииЗатрат;
using Handler = XMS.Integration.OneC.Ut.Abstractions.ICatalog_КСЗ_КатегорииЗатрат_NotificationHandler;

namespace XMS.Integration.OneC.Ut.Features.Catalog_КСЗ_КатегорииЗатрат_Feature;

internal class Catalog_КСЗ_КатегорииЗатрат_NotificationConsumer(
    IConnectionFactory factory,
    IServiceProvider serviceProvider,
    IHostEnvironment hostEnvironment,
    ILogger<Catalog_Номенклатура_NotificationConsumer> logger)
    : CatalogNotificationConsumer<Entity, Entity, Handler>(factory, serviceProvider, hostEnvironment, logger)
{ }
