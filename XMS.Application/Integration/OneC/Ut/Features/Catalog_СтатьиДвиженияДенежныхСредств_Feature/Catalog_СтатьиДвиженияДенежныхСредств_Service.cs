using Microsoft.Extensions.Logging;
using Entity = XMS.Application.Integration.OneC.Ut.Features.Catalog_СтатьиДвиженияДенежныхСредств_Feature.Catalog_СтатьиДвиженияДенежныхСредств;

namespace XMS.Application.Integration.OneC.Ut.Features.Catalog_СтатьиДвиженияДенежныхСредств_Feature;

internal class Catalog_СтатьиДвиженияДенежныхСредств_Service(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_СтатьиДвиженияДенежныхСредств_Service> logger)
    : CatalogService<Entity>(utClient, dbFactory, logger), ICatalog_СтатьиДвиженияДенежныхСредств_Service
{ }