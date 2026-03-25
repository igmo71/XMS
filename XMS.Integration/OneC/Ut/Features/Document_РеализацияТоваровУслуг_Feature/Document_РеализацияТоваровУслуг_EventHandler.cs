using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.ODataClient;
using Entity = XMS.Integration.OneC.Ut.Features.Document_РеализацияТоваровУслуг_Feature.Document_РеализацияТоваровУслуг;

namespace XMS.Integration.OneC.Ut.Features.Document_РеализацияТоваровУслуг_Feature;

internal class Document_РеализацияТоваровУслуг_EventHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Document_РеализацияТоваровУслуг_EventHandler> logger)
    : DocumentEventHandler<Entity, DocumentEvent>(utClient, dbFactory, logger), IDocument_РеализацияТоваровУслуг_EventHandler
{ }
