using Microsoft.Extensions.Logging;
using Entity = XMS.Application.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature.Catalog_Партнеры;

namespace XMS.Application.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature;

internal class Catalog_Партнеры_Service(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_Партнеры_Service> logger)
    : CatalogService<Entity>(utClient, dbFactory, logger), ICatalog_Партнеры_Service
{ }
