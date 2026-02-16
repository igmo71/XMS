using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Services
{
    public interface ICashFlowItemService
    {
        Task<IReadOnlyList<CashFlowItem>> GetListAsync(bool ignoreQueryFilters = false, CancellationToken ct = default);
        Task<IReadOnlyList<CashFlowItem>> LoadListAsync(CancellationToken ct = default);
        Task SaveListAsync(IReadOnlyList<CashFlowItem> list, CancellationToken ct = default);
        Task ReloadListAsync(CancellationToken ct = default);
    }
}
