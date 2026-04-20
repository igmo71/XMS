using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Integration.OneC.Events;

namespace XMS.Application.Integration.OneC.Ut.Features.Catalog_Пользователи_Feature;

internal class Catalog_Пользователи_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_Пользователи_NotificationHandler> logger)
    : CatalogNotificationHandler<Catalog_Пользователи>(utClient, dbFactory, logger),
      IIntegrationEventHandler<Catalog_Пользователи>
{ }
