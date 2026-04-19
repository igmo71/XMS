using Microsoft.Extensions.Logging;
using Entity = XMS.Application.Integration.OneC.Ut.Features.Catalog_Номенклатура_Feature.Catalog_Номенклатура;

namespace XMS.Application.Integration.OneC.Ut.Features.Catalog_Номенклатура_Feature;

internal class Catalog_Номенклатура_Service(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_Номенклатура_Service> logger)
    : CatalogService<Entity>(utClient, dbFactory, logger), ICatalog_Номенклатура_Service
{ }
