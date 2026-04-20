using Microsoft.Extensions.Logging;

namespace XMS.Application.Integration.OneC.Ut.Features.Catalog_Номенклатура_Feature;

internal class Catalog_Номенклатура_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    IAppEventPublisher appEventPublisher,
    ILogger<Catalog_Номенклатура_NotificationHandler> logger)
    : CatalogNotificationHandler<Catalog_Номенклатура>(utClient, dbFactory, appEventPublisher, logger)
{ }
