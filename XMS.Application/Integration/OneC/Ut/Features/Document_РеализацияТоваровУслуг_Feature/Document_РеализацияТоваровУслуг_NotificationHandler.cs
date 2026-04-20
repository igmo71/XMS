using Microsoft.Extensions.Logging;

namespace XMS.Application.Integration.OneC.Ut.Features.Document_РеализацияТоваровУслуг_Feature;

internal class Document_РеализацияТоваровУслуг_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    IAppEventPublisher appEventPublisher,
    ILogger<Document_РеализацияТоваровУслуг_NotificationHandler> logger)
    : DocumentNotificationHandler<Document_РеализацияТоваровУслуг>(utClient, dbFactory, appEventPublisher, logger)
{ }
