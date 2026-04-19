using Microsoft.Extensions.Logging;
using Entity = XMS.Application.Integration.OneC.Ut.Features.Document_ЗаказКлиента_Feature.Document_ЗаказКлиента;

namespace XMS.Application.Integration.OneC.Ut.Features.Document_ЗаказКлиента_Feature;

public record Document_ЗаказКлиента_Notification : DocumentNotification;

internal class Document_ЗаказКлиента_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Document_ЗаказКлиента_NotificationHandler> logger)
    : DocumentNotificationHandler<Entity, Document_ЗаказКлиента_Notification>(utClient, dbFactory, logger),
      IIntegrationEventHandler<Document_ЗаказКлиента_Notification>
{ }