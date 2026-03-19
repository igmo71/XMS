using XMS.Core.Common;
using XMS.Integration.OneC.Ut.Models;

namespace XMS.Integration.OneC.Ut.Abstractions
{
    internal interface ICatalog_СтатьиДвиженияДенежныхСредств_Service
    {
        Task<ServiceResult> CreateOrUpdateAsync(Guid refKey, CancellationToken ct);

        Task<ServiceResult> DeleteAsync(Guid refKey, CancellationToken ct);

        Task<Document_СписаниеБезналичныхДенежныхСредств?> GetAsync(Guid refKey, CancellationToken ct);

        Task<IReadOnlyList<Catalog_СтатьиДвиженияДенежныхСредств>> GetListAsync(bool includeDeleted = false, CancellationToken ct = default);
        
        Task<ServiceResult> ResyncAsync(CancellationToken ct = default);
    }
}
