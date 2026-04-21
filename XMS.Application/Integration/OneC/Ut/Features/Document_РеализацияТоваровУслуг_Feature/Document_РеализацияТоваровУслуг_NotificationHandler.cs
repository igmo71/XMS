using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Data;
using XMS.Application.Abstractions.EventBus;
using XMS.Application.Integration.OneC.Common;
using XMS.Application.Integration.OneC.Ut.ODataClient;

namespace XMS.Application.Integration.OneC.Ut.Features.Document_РеализацияТоваровУслуг_Feature;

internal class Document_РеализацияТоваровУслуг_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    IAppEventPublisher appEventPublisher,
    ILogger<Document_РеализацияТоваровУслуг_NotificationHandler> logger,
    IConfiguration configuration)
    : DocumentNotificationHandler<Document_РеализацияТоваровУслуг>(utClient, dbFactory, appEventPublisher, logger, configuration)
{ }
