using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Integration.OneC.Events;

namespace XMS.Application.Integration.OneC.Ut.Features.Document_ЗаказКлиента_Feature;

internal class Document_ЗаказКлиента_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Document_ЗаказКлиента_NotificationHandler> logger)
    : DocumentNotificationHandler<Document_ЗаказКлиента>(utClient, dbFactory, logger),
      IIntegrationEventHandler<Document_ЗаказКлиента>
{ }