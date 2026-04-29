using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Data;
using XMS.Application.Abstractions.EventBus;
using XMS.Integrations.OneC.Common;
using XMS.Integrations.OneC.Ut.ODataClient;

namespace XMS.Integrations.OneC.Ut.Features.Catalog_Номенклатура_Feature;

internal class Catalog_Номенклатура_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    IAppEventPublisher appEventPublisher,
    ILogger<Catalog_Номенклатура_NotificationHandler> logger)
    : CatalogNotificationHandler<Catalog_Номенклатура>(utClient, dbFactory, appEventPublisher, logger)
{ }
