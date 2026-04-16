using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Integration.Abstractions;
using XMS.Integration.OneC.Common;
using XMS.Integration.OneC.Ut.ODataClient;
using Entity = XMS.Integration.OneC.Ut.Features.Catalog_Номенклатура_Feature.Catalog_Номенклатура;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Номенклатура_Feature;

public record Catalog_Номенклатура_Notification : CatalogNotification;

internal class Catalog_Номенклатура_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_Номенклатура_NotificationHandler> logger)
    : CatalogNotificationHandler<Entity, Catalog_Номенклатура_Notification>(utClient, dbFactory, logger),
      IIntegrationEventHandler<Catalog_Номенклатура_Notification>
{ }
