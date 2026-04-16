using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Integration.Abstractions;
using XMS.Integration.OneC.Common;
using XMS.Integration.OneC.Ut.ODataClient;
using Entity = XMS.Integration.OneC.Ut.Features.Catalog_Пользователи_Feature.Catalog_Пользователи;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Пользователи_Feature;

public record Catalog_Пользователи_Notification : CatalogNotification;

internal class Catalog_Пользователи_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_Пользователи_NotificationHandler> logger)
    : CatalogNotificationHandler<Entity, Catalog_Пользователи_Notification>(utClient, dbFactory, logger),
      IIntegrationEventHandler<Catalog_Пользователи_Notification>
{ }
