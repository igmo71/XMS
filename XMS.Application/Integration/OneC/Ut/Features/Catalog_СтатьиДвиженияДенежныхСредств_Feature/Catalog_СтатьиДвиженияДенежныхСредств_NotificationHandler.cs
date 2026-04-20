using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Integration.OneC.Events;

namespace XMS.Application.Integration.OneC.Ut.Features.Catalog_СтатьиДвиженияДенежныхСредств_Feature;

internal class Catalog_СтатьиДвиженияДенежныхСредств_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_СтатьиДвиженияДенежныхСредств_NotificationHandler> logger)
    : CatalogNotificationHandler<Catalog_СтатьиДвиженияДенежныхСредств>(utClient, dbFactory, logger),
      IIntegrationEventHandler<Catalog_СтатьиДвиженияДенежныхСредств>
{ }