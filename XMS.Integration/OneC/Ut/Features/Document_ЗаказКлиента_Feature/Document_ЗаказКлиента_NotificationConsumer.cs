using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using XMS.Integration.OneC.Common;
using Entity = XMS.Integration.OneC.Ut.Features.Document_ЗаказКлиента_Feature.Document_ЗаказКлиента;
using Handler = XMS.Integration.OneC.Ut.Abstractions.IDocument_ЗаказКлиента_NotificationHandler;

namespace XMS.Integration.OneC.Ut.Features.Document_ЗаказКлиента_Feature;

internal class Document_ЗаказКлиента_NotificationConsumer(
    IConnectionFactory factory,
    IServiceProvider serviceProvider,
    IHostEnvironment hostEnvironment,
    ILogger<Document_ЗаказКлиента_NotificationConsumer> logger)
    : DocumentNotificationConsumer<Entity, DocumentNotification, Handler>(factory, serviceProvider, hostEnvironment, logger)
{ }