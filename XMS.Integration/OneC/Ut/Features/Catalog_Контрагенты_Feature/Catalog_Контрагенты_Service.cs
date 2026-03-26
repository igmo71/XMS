using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.ODataClient;
using Entity = XMS.Integration.OneC.Ut.Features.Catalog_Контрагенты_Feature.Catalog_Контрагенты;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Контрагенты_Feature;

internal class Catalog_Контрагенты_Service(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_Контрагенты_Service> logger)
    : CatalogService<Entity>(utClient, dbFactory, logger), ICatalog_Контрагенты_Service
{ }