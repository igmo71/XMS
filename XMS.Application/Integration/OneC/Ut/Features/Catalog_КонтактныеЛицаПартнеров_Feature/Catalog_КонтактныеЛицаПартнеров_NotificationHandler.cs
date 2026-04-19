using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Data;
using XMS.Application.Abstractions.Integration.Events;
using XMS.Application.Integration.OneC.Common;
using XMS.Application.Integration.OneC.Ut.ODataClient;
using Entity = XMS.Application.Integration.OneC.Ut.Features.Catalog_КонтактныеЛицаПартнеров_Feature.Catalog_КонтактныеЛицаПартнеров;

namespace XMS.Application.Integration.OneC.Ut.Features.Catalog_КонтактныеЛицаПартнеров_Feature;

public record Catalog_КонтактныеЛицаПартнеров_Notification : CatalogNotification;

internal class Catalog_КонтактныеЛицаПартнеров_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_КонтактныеЛицаПартнеров_NotificationHandler> logger)
    : CatalogNotificationHandler<Entity, Catalog_КонтактныеЛицаПартнеров_Notification>(utClient, dbFactory, logger),
      IIntegrationEventHandler<Catalog_КонтактныеЛицаПартнеров_Notification>
{ }
