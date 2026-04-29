using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Data;
using XMS.Integrations.OneC.Ut;
using XMS.Integrations.OneC.Common;
using XMS.Integrations.OneC.Ut.ODataClient;

namespace XMS.Integrations.OneC.Ut.Features.Catalog_Пользователи_Feature;

internal class Catalog_Пользователи_Service(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_Пользователи_Service> logger)
    : CatalogService<Catalog_Пользователи>(utClient, dbFactory, logger), ICatalog_Пользователи_Service
{ }
