using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.ODataClient;
using Entity = XMS.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature.Catalog_Партнеры;
using Event = XMS.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature.Catalog_Партнеры_Changed;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature;

internal class Catalog_Партнеры_EventHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_Партнеры_EventHandler> logger)
    : CatalogEventHandler<Entity, Event>(utClient, dbFactory, logger), ICatalog_Партнеры_EventHandler
{ }
