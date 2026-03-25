using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.ODataClient;
using Entity = XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature.Document_СписаниеБезналичныхДенежныхСредств;
using Event = XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature.Document_СписаниеБезналичныхДенежныхСредств_Changed;

namespace XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;

internal class Document_СписаниеБезналичныхДенежныхСредств_EventHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Document_СписаниеБезналичныхДенежныхСредств_EventHandler> logger)
    : DocumentEventHandler<Entity, Event>(utClient, dbFactory, logger), IDocument_СписаниеБезналичныхДенежныхСредств_EventHandler
{ }
