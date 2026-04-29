using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Data;
using XMS.Integrations.OneC.Ut;
using XMS.Integrations.OneC.Common;
using XMS.Integrations.OneC.Ut.ODataClient;

namespace XMS.Integrations.OneC.Ut.Features.Catalog_СтатьиДвиженияДенежныхСредств_Feature;

internal class Catalog_СтатьиДвиженияДенежныхСредств_Service(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_СтатьиДвиженияДенежныхСредств_Service> logger)
    : CatalogService<Catalog_СтатьиДвиженияДенежныхСредств>(utClient, dbFactory, logger), ICatalog_СтатьиДвиженияДенежныхСредств_Service
{ }