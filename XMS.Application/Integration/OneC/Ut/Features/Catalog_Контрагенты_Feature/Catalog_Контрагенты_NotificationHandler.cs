using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Integration.OneC.Events;

namespace XMS.Application.Integration.OneC.Ut.Features.Catalog_Контрагенты_Feature;

internal class Catalog_Контрагенты_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_Контрагенты_NotificationHandler> logger)
    : CatalogNotificationHandler<Catalog_Контрагенты>(utClient, dbFactory, logger),
      IIntegrationEventHandler<Catalog_Контрагенты>
{ }