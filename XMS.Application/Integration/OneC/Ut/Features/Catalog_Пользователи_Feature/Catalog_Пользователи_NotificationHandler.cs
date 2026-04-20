using Microsoft.Extensions.Logging;

namespace XMS.Application.Integration.OneC.Ut.Features.Catalog_Пользователи_Feature;

internal class Catalog_Пользователи_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    IAppEventPublisher appEventPublisher,
    ILogger<Catalog_Пользователи_NotificationHandler> logger)
    : CatalogNotificationHandler<Catalog_Пользователи>(utClient, dbFactory, appEventPublisher, logger)
{ }
