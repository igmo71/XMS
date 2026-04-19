using Microsoft.Extensions.Logging;
using Entity = XMS.Application.Integration.OneC.Ut.Features.Catalog_Пользователи_Feature.Catalog_Пользователи;

namespace XMS.Application.Integration.OneC.Ut.Features.Catalog_Пользователи_Feature;

internal class Catalog_Пользователи_Service(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_Пользователи_Service> logger)
    : CatalogService<Entity>(utClient, dbFactory, logger), ICatalog_Пользователи_Service
{ }
