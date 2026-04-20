using Microsoft.Extensions.Logging;

namespace XMS.Application.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature;

internal class Document_РасходныйКассовыйОрдер_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    IAppEventPublisher appEventPublisher,
    ILogger<Document_РасходныйКассовыйОрдер_NotificationHandler> logger)
    : DocumentNotificationHandler<Document_РасходныйКассовыйОрдер>(utClient, dbFactory, appEventPublisher, logger)
{ }