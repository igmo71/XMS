using Microsoft.Extensions.Logging;
using Entity = XMS.Application.Integration.OneC.Ut.Features.Catalog_КонтактныеЛицаПартнеров_Feature.Catalog_КонтактныеЛицаПартнеров;

namespace XMS.Application.Integration.OneC.Ut.Features.Catalog_КонтактныеЛицаПартнеров_Feature;

internal class Catalog_КонтактныеЛицаПартнеров_Service(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger<Catalog_КонтактныеЛицаПартнеров_Service> logger)
    : CatalogService<Entity>(utClient, dbFactory, logger), ICatalog_КонтактныеЛицаПартнеров_Service
{ }