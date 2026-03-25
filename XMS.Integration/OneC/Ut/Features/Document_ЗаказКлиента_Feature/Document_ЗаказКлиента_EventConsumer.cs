using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Entity = XMS.Integration.OneC.Ut.Features.Document_ЗаказКлиента_Feature.Document_ЗаказКлиента;
using Handler = XMS.Integration.OneC.Ut.Abstractions.IDocument_ЗаказКлиента_EventHandler;

namespace XMS.Integration.OneC.Ut.Features.Document_ЗаказКлиента_Feature;

internal class Document_ЗаказКлиента_EventConsumer(
    IConnectionFactory factory,
    IServiceProvider serviceProvider,
    IHostEnvironment hostEnvironment,
    ILogger<Document_ЗаказКлиента_EventConsumer> logger)
    : DocumentEventConsumer<Entity, DocumentEvent, Handler>(factory, serviceProvider, hostEnvironment, logger)
{ }