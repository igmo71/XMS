using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Data;
using XMS.Integrations.OneC.Ut;
using XMS.Integrations.OneC.Common;
using XMS.Integrations.OneC.Ut.ODataClient;

namespace XMS.Integrations.OneC.Ut.Features.Catalog_Номенклатура_Feature;

internal class Catalog_Номенклатура_Service(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_Номенклатура_Service> logger)
    : CatalogService<Catalog_Номенклатура>(utClient, dbFactory, logger), ICatalog_Номенклатура_Service
{ }
