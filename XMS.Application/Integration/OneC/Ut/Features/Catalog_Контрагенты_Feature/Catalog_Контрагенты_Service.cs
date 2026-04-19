using Microsoft.Extensions.Logging;
using Entity = XMS.Application.Integration.OneC.Ut.Features.Catalog_Контрагенты_Feature.Catalog_Контрагенты;

namespace XMS.Application.Integration.OneC.Ut.Features.Catalog_Контрагенты_Feature;

internal class Catalog_Контрагенты_Service(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_Контрагенты_Service> logger)
    : CatalogService<Entity>(utClient, dbFactory, logger), ICatalog_Контрагенты_Service
{ }