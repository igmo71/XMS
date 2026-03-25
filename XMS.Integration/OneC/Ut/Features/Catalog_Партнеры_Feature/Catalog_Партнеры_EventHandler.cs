using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.ODataClient;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature;

internal class Catalog_Партнеры_EventHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_Партнеры_EventHandler> logger)
    : CatalogEventHandler<Catalog_Партнеры, Catalog_Партнеры_Changed>(utClient, dbFactory, logger), ICatalog_Партнеры_EventHandler
{ }
