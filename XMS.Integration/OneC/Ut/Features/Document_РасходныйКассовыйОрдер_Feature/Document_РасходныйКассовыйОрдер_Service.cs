using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Integration.OneC.Common;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.ODataClient;
using Entity = XMS.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature.Document_РасходныйКассовыйОрдер;

namespace XMS.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature;

internal class Document_РасходныйКассовыйОрдер_Service(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Document_РасходныйКассовыйОрдер_Service> logger)
    : DocumentService<Entity>(utClient, dbFactory, logger), IDocument_РасходныйКассовыйОрдер_Service
{ }
