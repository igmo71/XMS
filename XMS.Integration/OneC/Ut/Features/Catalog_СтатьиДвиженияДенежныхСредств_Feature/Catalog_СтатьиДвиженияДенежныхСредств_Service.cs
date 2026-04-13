using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Integration.OneC.Common;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.ODataClient;
using Entity = XMS.Integration.OneC.Ut.Features.Catalog_СтатьиДвиженияДенежныхСредств_Feature.Catalog_СтатьиДвиженияДенежныхСредств;

namespace XMS.Integration.OneC.Ut.Features.Catalog_СтатьиДвиженияДенежныхСредств_Feature;

internal class Catalog_СтатьиДвиженияДенежныхСредств_Service(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_СтатьиДвиженияДенежныхСредств_Service> logger)
    : CatalogService<Entity>(utClient, dbFactory, logger), ICatalog_СтатьиДвиженияДенежныхСредств_Service
{ }