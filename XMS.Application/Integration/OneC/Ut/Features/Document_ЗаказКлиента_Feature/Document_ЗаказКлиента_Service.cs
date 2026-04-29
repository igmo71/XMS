using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Data;
using XMS.Integrations.OneC.Ut;
using XMS.Integrations.OneC.Common;
using XMS.Integrations.OneC.Ut.ODataClient;

namespace XMS.Integrations.OneC.Ut.Features.Document_ЗаказКлиента_Feature;

internal class Document_ЗаказКлиента_Service(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Document_ЗаказКлиента_Service> logger)
    : DocumentService<Document_ЗаказКлиента>(utClient, dbFactory, logger), IDocument_ЗаказКлиента_Service
{ }