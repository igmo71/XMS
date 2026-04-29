using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Data;
using XMS.Application.Abstractions.EventBus;
using XMS.Integrations.OneC.Common;
using XMS.Integrations.OneC.Ut.ODataClient;

namespace XMS.Integrations.OneC.Ut.Features.Catalog_СтатьиДвиженияДенежныхСредств_Feature;

internal class Catalog_СтатьиДвиженияДенежныхСредств_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    IAppEventPublisher appEventPublisher,
    ILogger<Catalog_СтатьиДвиженияДенежныхСредств_NotificationHandler> logger)
    : CatalogNotificationHandler<Catalog_СтатьиДвиженияДенежныхСредств>(utClient, dbFactory, appEventPublisher, logger)
{ }