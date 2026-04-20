using Microsoft.Extensions.Logging;

namespace XMS.Application.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;

internal class Document_СписаниеБезналичныхДенежныхСредств_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    IAppEventPublisher appEventPublisher,
    ILogger<Document_СписаниеБезналичныхДенежныхСредств_NotificationHandler> logger)
    : DocumentNotificationHandler<Document_СписаниеБезналичныхДенежныхСредств>(utClient, dbFactory, appEventPublisher, logger)
{ }
