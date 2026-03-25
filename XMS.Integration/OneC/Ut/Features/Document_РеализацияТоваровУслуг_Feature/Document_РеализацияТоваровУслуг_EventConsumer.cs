using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Entity = XMS.Integration.OneC.Ut.Features.Document_РеализацияТоваровУслуг_Feature.Document_РеализацияТоваровУслуг;
using Handler = XMS.Integration.OneC.Ut.Abstractions.IDocument_РеализацияТоваровУслуг_EventHandler;

namespace XMS.Integration.OneC.Ut.Features.Document_РеализацияТоваровУслуг_Feature;

internal class Document_РеализацияТоваровУслуг_EventConsumer(
    IConnectionFactory factory,
    IServiceProvider serviceProvider,
    IHostEnvironment hostEnvironment,
    ILogger<Document_РеализацияТоваровУслуг_EventConsumer> logger)
    : DocumentEventConsumer<Entity, DocumentEvent, Handler>(factory, serviceProvider, hostEnvironment, logger)
{ }
