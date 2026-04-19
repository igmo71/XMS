using Microsoft.Extensions.Logging;
using Entity = XMS.Application.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature.Document_РасходныйКассовыйОрдер;

namespace XMS.Application.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature;

internal class Document_РасходныйКассовыйОрдер_Service(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Document_РасходныйКассовыйОрдер_Service> logger)
    : DocumentService<Entity>(utClient, dbFactory, logger), IDocument_РасходныйКассовыйОрдер_Service
{ }
