using Microsoft.Extensions.Logging;

namespace XMS.Application.Integration.OneC.Ut.Features.Document_ЗаказКлиента_Feature;

internal class Document_ЗаказКлиента_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    IAppEventPublisher appEventPublisher,
    ILogger<Document_ЗаказКлиента_NotificationHandler> logger)
    : DocumentNotificationHandler<Document_ЗаказКлиента>(utClient, dbFactory, appEventPublisher, logger)
{ }