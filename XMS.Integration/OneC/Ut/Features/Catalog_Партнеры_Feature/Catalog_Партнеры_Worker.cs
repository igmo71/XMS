using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using XMS.Integration.OneC.Ut.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature;

public class Catalog_Партнеры_Worker(IConnectionFactory factory, IServiceProvider serviceProvider, ILogger<Catalog_Партнеры_Worker> logger)
    : OneCCatalogWorker<Catalog_Партнеры, Catalog_Партнеры_Changed, ICatalog_Партнеры_Service>(factory, serviceProvider, logger)
{ }