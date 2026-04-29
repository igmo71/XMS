using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Data;
using XMS.Application.Abstractions.EventBus;
using XMS.Integrations.OneC.Common;
using XMS.Integrations.OneC.Ut.ODataClient;

namespace XMS.Integrations.OneC.Ut.Features.Catalog_Партнеры_Feature;

internal class Catalog_Партнеры_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    IAppEventPublisher appEventPublisher,
    ILogger<Catalog_Партнеры_NotificationHandler> logger)
    : CatalogNotificationHandler<Catalog_Партнеры>(utClient, dbFactory, appEventPublisher, logger)
{ }
