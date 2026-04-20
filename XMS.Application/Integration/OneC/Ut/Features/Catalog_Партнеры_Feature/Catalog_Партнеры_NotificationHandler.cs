using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Integration.OneC.Events;

namespace XMS.Application.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature;

internal class Catalog_Партнеры_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_Партнеры_NotificationHandler> logger)
    : CatalogNotificationHandler<Catalog_Партнеры>(utClient, dbFactory, logger),
      IIntegrationEventHandler<Catalog_Партнеры>
{ }
