using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Data;
using XMS.Integrations.OneC.Ut;
using XMS.Integrations.OneC.Common;
using XMS.Integrations.OneC.Ut.ODataClient;

namespace XMS.Integrations.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature;

internal class Document_РасходныйКассовыйОрдер_Service(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Document_РасходныйКассовыйОрдер_Service> logger)
    : DocumentService<Document_РасходныйКассовыйОрдер>(utClient, dbFactory, logger), IDocument_РасходныйКассовыйОрдер_Service
{ }
