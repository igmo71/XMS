using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Integration.OneC.Events;

namespace XMS.Application.Integration.OneC.Ut.Features.Catalog_КонтактныеЛицаПартнеров_Feature;

internal class Catalog_КонтактныеЛицаПартнеров_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_КонтактныеЛицаПартнеров_NotificationHandler> logger)
    : CatalogNotificationHandler<Catalog_КонтактныеЛицаПартнеров>(utClient, dbFactory, logger),
      IIntegrationEventHandler<Catalog_КонтактныеЛицаПартнеров>
{ }
