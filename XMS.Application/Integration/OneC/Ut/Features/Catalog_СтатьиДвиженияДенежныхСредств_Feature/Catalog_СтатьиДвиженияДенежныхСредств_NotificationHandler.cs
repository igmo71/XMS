using Microsoft.Extensions.Logging;

namespace XMS.Application.Integration.OneC.Ut.Features.Catalog_СтатьиДвиженияДенежныхСредств_Feature;

internal class Catalog_СтатьиДвиженияДенежныхСредств_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    IAppEventPublisher appEventPublisher,
    ILogger<Catalog_СтатьиДвиженияДенежныхСредств_NotificationHandler> logger)
    : CatalogNotificationHandler<Catalog_СтатьиДвиженияДенежныхСредств>(utClient, dbFactory, appEventPublisher, logger)
{ }