using Microsoft.Extensions.Logging;
using Entity = XMS.Application.Integration.OneC.Ut.Features.Document_ЗаказКлиента_Feature.Document_ЗаказКлиента;

namespace XMS.Application.Integration.OneC.Ut.Features.Document_ЗаказКлиента_Feature;

internal class Document_ЗаказКлиента_Service(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Document_ЗаказКлиента_Service> logger)
    : DocumentService<Entity>(utClient, dbFactory, logger), IDocument_ЗаказКлиента_Service
{ }