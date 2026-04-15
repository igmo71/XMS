using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using XMS.Integration.OneC.Common;
using Entity = XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature.Document_СписаниеБезналичныхДенежныхСредств;
using Handler = XMS.Integration.OneC.Ut.Abstractions.IDocument_СписаниеБезналичныхДенежныхСредств_NotificationHandler;

namespace XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;

internal class Document_СписаниеБезналичныхДенежныхСредств_NotificationConsumer(
    IConnectionFactory factory,
    IServiceProvider serviceProvider,
    IHostEnvironment hostEnvironment,
    ILogger<Document_СписаниеБезналичныхДенежныхСредств_NotificationConsumer> logger)
    : DocumentNotificationConsumer<Entity, DocumentNotification, Handler>(factory, serviceProvider, hostEnvironment, logger)
{ }
