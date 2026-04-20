using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Integration.OneC.Events;

namespace XMS.Application.Integration.OneC.Ut.Features.Catalog_Номенклатура_Feature;

internal class Catalog_Номенклатура_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_Номенклатура_NotificationHandler> logger)
    : CatalogNotificationHandler<Catalog_Номенклатура>(utClient, dbFactory, logger),
      IIntegrationEventHandler<Catalog_Номенклатура>
{ }
