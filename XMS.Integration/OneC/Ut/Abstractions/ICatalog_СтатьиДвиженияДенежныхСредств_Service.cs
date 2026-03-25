using XMS.Core.Common;
using XMS.Integration.OneC.Ut.Features.Catalog_СтатьиДвиженияДенежныхСредств_Feature;

namespace XMS.Integration.OneC.Ut.Abstractions;

internal interface ICatalog_СтатьиДвиженияДенежныхСредств_Service
{
    Task<ServiceResult> CreateOrUpdateAsync(Guid refKey, CancellationToken ct = default);

    Task<ServiceResult> DeleteAsync(Guid refKey, CancellationToken ct = default);



    Task<ServiceResult> HandleEventOneC(Catalog_СтатьиДвиженияДенежныхСредств_Changed message, CancellationToken ct = default);

    Task<Catalog_СтатьиДвиженияДенежныхСредств?> GetAsync(Guid refKey, CancellationToken ct = default);

    Task<IReadOnlyList<Catalog_СтатьиДвиженияДенежныхСредств>> GetListAsync(CatalogQueryParameters parameters, CancellationToken ct = default);

    Task<ServiceResult> ResyncAsync(CancellationToken ct = default);
}
