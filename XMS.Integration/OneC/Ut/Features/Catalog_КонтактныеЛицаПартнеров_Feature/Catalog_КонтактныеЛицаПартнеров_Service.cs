using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Integration.OneC.Common;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.ODataClient;
using Entity = XMS.Integration.OneC.Ut.Features.Catalog_КонтактныеЛицаПартнеров_Feature.Catalog_КонтактныеЛицаПартнеров;

namespace XMS.Integration.OneC.Ut.Features.Catalog_КонтактныеЛицаПартнеров_Feature;

internal class Catalog_КонтактныеЛицаПартнеров_Service(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_КонтактныеЛицаПартнеров_Service> logger)
    : CatalogService<Entity>(utClient, dbFactory, logger), ICatalog_КонтактныеЛицаПартнеров_Service
{ }