using Microsoft.Extensions.Logging;

namespace XMS.Application.Integration.OneC.Ut.Features.Catalog_КонтактныеЛицаПартнеров_Feature;

internal class Catalog_КонтактныеЛицаПартнеров_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    IAppEventPublisher appEventPublisher,
    ILogger<Catalog_КонтактныеЛицаПартнеров_NotificationHandler> logger)
    : CatalogNotificationHandler<Catalog_КонтактныеЛицаПартнеров>(utClient, dbFactory, appEventPublisher, logger)
{ }
