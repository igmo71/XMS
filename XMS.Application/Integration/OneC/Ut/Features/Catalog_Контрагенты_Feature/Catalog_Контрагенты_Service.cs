using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Data;
using XMS.Integrations.OneC.Ut;
using XMS.Integrations.OneC.Common;
using XMS.Integrations.OneC.Ut.ODataClient;


namespace XMS.Integrations.OneC.Ut.Features.Catalog_Контрагенты_Feature;

internal class Catalog_Контрагенты_Service(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_Контрагенты_Service> logger)
    : CatalogService<Catalog_Контрагенты>(utClient, dbFactory, logger), ICatalog_Контрагенты_Service
{ }