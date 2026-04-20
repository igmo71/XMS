using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Data;
using XMS.Application.Abstractions.Integration.OneC.Ut;
using XMS.Application.Integration.OneC.Common;
using XMS.Application.Integration.OneC.Ut.ODataClient;

namespace XMS.Application.Integration.OneC.Ut.Features.Catalog_Номенклатура_Feature;

internal class Catalog_Номенклатура_Service(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_Номенклатура_Service> logger)
    : CatalogService<Catalog_Номенклатура>(utClient, dbFactory, logger), ICatalog_Номенклатура_Service
{ }
