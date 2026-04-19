using Microsoft.Extensions.Logging;
using Entity = XMS.Application.Integration.OneC.Ut.Features.Catalog_СтатьиДвиженияДенежныхСредств_Feature.Catalog_СтатьиДвиженияДенежныхСредств;

namespace XMS.Application.Integration.OneC.Ut.Features.Catalog_СтатьиДвиженияДенежныхСредств_Feature;

public record Catalog_СтатьиДвиженияДенежныхСредств_Notification : CatalogNotification;

internal class Catalog_СтатьиДвиженияДенежныхСредств_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_СтатьиДвиженияДенежныхСредств_NotificationHandler> logger)
    : CatalogNotificationHandler<Entity, Catalog_СтатьиДвиженияДенежныхСредств_Notification>(utClient, dbFactory, logger),
      IIntegrationEventHandler<Catalog_СтатьиДвиженияДенежныхСредств_Notification>
{ }