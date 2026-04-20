using Microsoft.Extensions.Logging;

namespace XMS.Application.Integration.OneC.Ut.Features.Catalog_Контрагенты_Feature;

internal class Catalog_Контрагенты_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    IAppEventPublisher appEventPublisher,
    ILogger<Catalog_Контрагенты_NotificationHandler> logger)
    : CatalogNotificationHandler<Catalog_Контрагенты>(utClient, dbFactory, appEventPublisher, logger)
{ }