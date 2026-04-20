using Microsoft.Extensions.Logging;

namespace XMS.Application.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature;

internal class Catalog_Партнеры_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    IAppEventPublisher appEventPublisher,
    ILogger<Catalog_Партнеры_NotificationHandler> logger)
    : CatalogNotificationHandler<Catalog_Партнеры>(utClient, dbFactory, appEventPublisher, logger)
{ }
