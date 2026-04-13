using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Integration.OneC.Common;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.ODataClient;
using Entity = XMS.Integration.OneC.Ut.Features.Document_ЗаказКлиента_Feature.Document_ЗаказКлиента;

namespace XMS.Integration.OneC.Ut.Features.Document_ЗаказКлиента_Feature;

internal class Document_ЗаказКлиента_Service(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Document_ЗаказКлиента_Service> logger)
    : DocumentService<Entity>(utClient, dbFactory, logger), IDocument_ЗаказКлиента_Service
{ }