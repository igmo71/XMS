using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.ODataClient;
using Entity = XMS.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature.Document_РасходныйКассовыйОрдер;

namespace XMS.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature;

internal class Document_РасходныйКассовыйОрдер_EventHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Document_РасходныйКассовыйОрдер_EventHandler> logger)
    : DocumentEventHandler<Entity, DocumentEvent>(utClient, dbFactory, logger), IDocument_РасходныйКассовыйОрдер_EventHandler
{ }
