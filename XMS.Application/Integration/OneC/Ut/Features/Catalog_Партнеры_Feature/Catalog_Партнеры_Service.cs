using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Data;
using XMS.Integrations.OneC.Ut;
using XMS.Integrations.OneC.Common;
using XMS.Integrations.OneC.Ut.ODataClient;

namespace XMS.Integrations.OneC.Ut.Features.Catalog_Партнеры_Feature;

internal class Catalog_Партнеры_Service(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_Партнеры_Service> logger)
    : CatalogService<Catalog_Партнеры>(utClient, dbFactory, logger), ICatalog_Партнеры_Service
{ }
