using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Integration.OneC.Common;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.ODataClient;
using Entity = XMS.Integration.OneC.Ut.Features.Catalog_Пользователи_Feature.Catalog_Пользователи;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Пользователи_Feature;

internal class Catalog_Пользователи_EventHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_Пользователи_EventHandler> logger)
    : CatalogEventHandler<Entity, CatalogEvent>(utClient, dbFactory, logger), ICatalog_Пользователи_EventHandler
{ }
