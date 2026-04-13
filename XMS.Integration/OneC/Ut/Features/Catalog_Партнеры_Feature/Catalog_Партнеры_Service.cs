using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Integration.OneC.Common;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.ODataClient;
using Entity = XMS.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature.Catalog_Партнеры;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature;

internal class Catalog_Партнеры_Service(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_Партнеры_Service> logger)
    : CatalogService<Entity>(utClient, dbFactory, logger), ICatalog_Партнеры_Service
{ }
