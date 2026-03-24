using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Integration.OneC.Ut.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature
{
    internal class Catalog_Партнеры_Service(UtClient utClient, IDbContextFactoryProxy dbFactory, ILogger<Catalog_Партнеры_Service> logger)
        : OneCCatalogService<Catalog_Партнеры, Catalog_Партнеры_Changed>(utClient, dbFactory, logger), ICatalog_Партнеры_Service
    { }
}
