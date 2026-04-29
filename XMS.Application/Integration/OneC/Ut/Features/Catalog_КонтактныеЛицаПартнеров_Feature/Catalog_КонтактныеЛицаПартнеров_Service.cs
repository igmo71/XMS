using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Data;
using XMS.Integrations.OneC.Ut;
using XMS.Integrations.OneC.Common;
using XMS.Integrations.OneC.Ut.ODataClient;

namespace XMS.Integrations.OneC.Ut.Features.Catalog_КонтактныеЛицаПартнеров_Feature;

internal class Catalog_КонтактныеЛицаПартнеров_Service(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_КонтактныеЛицаПартнеров_Service> logger)
    : CatalogService<Catalog_КонтактныеЛицаПартнеров>(utClient, dbFactory, logger), ICatalog_КонтактныеЛицаПартнеров_Service
{ }