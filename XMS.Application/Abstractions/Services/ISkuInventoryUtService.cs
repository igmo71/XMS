using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Services
{
    public interface ISkuInventoryUtService
    {
        Task<IReadOnlyList<SkuInventoryUt>> GetListAsync(CancellationToken ct = default);
        Task<IReadOnlyList<SkuInventoryUt>> LoadListAsync(CancellationToken ct = default);
        Task ReloadListAsync(CancellationToken ct = default);
    }
}
