using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.ODataClient;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature;

internal class Catalog_Партнеры_Service(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_Партнеры_Service> logger)
    : CatalogService<Catalog_Партнеры>(utClient, dbFactory, logger), ICatalog_Партнеры_Service
{ }
