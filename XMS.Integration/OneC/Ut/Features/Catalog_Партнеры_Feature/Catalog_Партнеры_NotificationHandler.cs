using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Integration.Abstractions;
using XMS.Integration.OneC.Common;
using XMS.Integration.OneC.Ut.ODataClient;
using Entity = XMS.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature.Catalog_Партнеры;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature;

public record Catalog_Партнеры_Notification : CatalogNotification;

internal class Catalog_Партнеры_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_Партнеры_NotificationHandler> logger)
    : CatalogNotificationHandler<Entity, Catalog_Партнеры_Notification>(utClient, dbFactory, logger),
      IIntegrationEventHandler<Catalog_Партнеры_Notification>
{ }
