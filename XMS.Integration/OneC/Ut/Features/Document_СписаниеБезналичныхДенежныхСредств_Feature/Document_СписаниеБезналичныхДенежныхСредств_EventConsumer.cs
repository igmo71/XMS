using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Entity = XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature.Document_СписаниеБезналичныхДенежныхСредств;
using Event = XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature.Document_СписаниеБезналичныхДенежныхСредств_Changed;
using Handler = XMS.Integration.OneC.Ut.Abstractions.IDocument_СписаниеБезналичныхДенежныхСредств_EventHandler;

namespace XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;

internal class Document_СписаниеБезналичныхДенежныхСредств_EventConsumer(
    IConnectionFactory factory,
    IServiceProvider serviceProvider,
    IHostEnvironment hostEnvironment,
    ILogger<Document_СписаниеБезналичныхДенежныхСредств_EventConsumer> logger)
    : DocumentEventConsumer<Entity, Event, Handler>(factory, serviceProvider, hostEnvironment, logger)
{ }
