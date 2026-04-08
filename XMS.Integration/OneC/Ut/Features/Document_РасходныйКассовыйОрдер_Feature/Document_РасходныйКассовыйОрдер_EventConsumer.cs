using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Entity = XMS.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature.Document_РасходныйКассовыйОрдер;
using Handler = XMS.Integration.OneC.Ut.Abstractions.IDocument_РасходныйКассовыйОрдер_EventHandler;

namespace XMS.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature;

internal class Document_РасходныйКассовыйОрдер_EventConsumer(
    IConnectionFactory factory,
    IServiceProvider serviceProvider,
    IHostEnvironment hostEnvironment,
    ILogger<Document_РасходныйКассовыйОрдер_EventConsumer> logger)
    : DocumentEventConsumer<Entity, DocumentEvent, Handler>(factory, serviceProvider, hostEnvironment, logger)
{ }
