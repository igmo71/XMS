using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Integration.Abstractions;
using XMS.Integration.OneC.Common;
using XMS.Integration.OneC.Ut.ODataClient;
using Entity = XMS.Integration.OneC.Ut.Features.Catalog_Контрагенты_Feature.Catalog_Контрагенты;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Контрагенты_Feature;

public record Catalog_Контрагенты_Notification : CatalogNotification;

internal class Catalog_Контрагенты_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_Контрагенты_NotificationHandler> logger)
    : CatalogNotificationHandler<Entity, Catalog_Контрагенты_Notification>(utClient, dbFactory, logger),
      IIntegrationEventHandler<Catalog_Контрагенты_Notification>
{ }