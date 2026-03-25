using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.ODataClient;
using Entity = XMS.Integration.OneC.Ut.Features.Catalog_Номенклатура_Feature.Catalog_Номенклатура;
using Event = XMS.Integration.OneC.Ut.Features.Catalog_Номенклатура_Feature.Catalog_Номенклатура_Changed;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Номенклатура_Feature;

internal class Catalog_Номенклатура_EventHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_Номенклатура_EventHandler> logger)
    : CatalogEventHandler<Entity, Event>(utClient, dbFactory, logger), ICatalog_Номенклатура_EventHandler
{ }
