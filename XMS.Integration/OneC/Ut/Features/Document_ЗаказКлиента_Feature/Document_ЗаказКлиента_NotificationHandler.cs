using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Integration.Abstractions;
using XMS.Integration.OneC.Common;
using XMS.Integration.OneC.Ut.ODataClient;
using Entity = XMS.Integration.OneC.Ut.Features.Document_ЗаказКлиента_Feature.Document_ЗаказКлиента;

namespace XMS.Integration.OneC.Ut.Features.Document_ЗаказКлиента_Feature;

public record Document_ЗаказКлиента_Notification : DocumentNotification;

internal class Document_ЗаказКлиента_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Document_ЗаказКлиента_NotificationHandler> logger)
    : DocumentNotificationHandler<Entity, Document_ЗаказКлиента_Notification>(utClient, dbFactory, logger),
      IIntegrationEventHandler<Document_ЗаказКлиента_Notification>
{ }