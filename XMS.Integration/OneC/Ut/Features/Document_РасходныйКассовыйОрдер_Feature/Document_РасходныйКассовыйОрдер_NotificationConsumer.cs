using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using XMS.Integration.OneC.Common;
using Entity = XMS.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature.Document_РасходныйКассовыйОрдер;
using Handler = XMS.Integration.OneC.Ut.Abstractions.IDocument_РасходныйКассовыйОрдер_NotificationHandler;

namespace XMS.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature;

internal class Document_РасходныйКассовыйОрдер_NotificationConsumer(
    IConnectionFactory factory,
    IServiceProvider serviceProvider,
    IHostEnvironment hostEnvironment,
    ILogger<Document_РасходныйКассовыйОрдер_NotificationConsumer> logger)
    : DocumentNotificationConsumer<Entity, DocumentNotification, Handler>(factory, serviceProvider, hostEnvironment, logger)
{ }
