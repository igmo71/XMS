using Microsoft.Extensions.Logging;
using Entity = XMS.Application.Integration.OneC.Ut.Features.Document_РеализацияТоваровУслуг_Feature.Document_РеализацияТоваровУслуг;

namespace XMS.Application.Integration.OneC.Ut.Features.Document_РеализацияТоваровУслуг_Feature;

internal class Document_РеализацияТоваровУслуг_Service(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Document_РеализацияТоваровУслуг_Service> logger)
    : DocumentService<Entity>(utClient, dbFactory, logger), IDocument_РеализацияТоваровУслуг_Service
{ }
