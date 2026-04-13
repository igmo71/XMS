using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Integration.OneC.Common;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.ODataClient;
using Entity = XMS.Integration.OneC.Ut.Features.Catalog_Контрагенты_Feature.Catalog_Контрагенты;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Контрагенты_Feature;

internal class Catalog_Контрагенты_EventHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_Контрагенты_EventHandler> logger)
    : CatalogEventHandler<Entity, CatalogEvent>(utClient, dbFactory, logger), ICatalog_Контрагенты_EventHandler
{ }