using XMS.Core.Common;
using XMS.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature;

namespace XMS.Integration.OneC.Ut.Abstractions
{
    public interface ICatalog_Партнеры_Service
    {
        Task<ServiceResult> HandleEventOneC(Catalog_Партнеры_Changed oneCNotifyMessage, CancellationToken ct = default);

        Task<Catalog_Партнеры?> GetAsync(Guid refKey, CancellationToken ct = default);

        Task<IReadOnlyList<Catalog_Партнеры>> GetListAsync(CatalogQueryParameters parameters, CancellationToken ct = default);

        Task<ServiceResult> ResyncByDateRangeAsync(CancellationToken ct = default);
    }
}
