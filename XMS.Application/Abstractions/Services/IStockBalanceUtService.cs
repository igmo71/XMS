using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Services
{
    public interface IStockBalanceUtService
    {
        Task<IReadOnlyList<StockBalanceUt>> GetListAsync(CancellationToken ct = default);
        Task<IReadOnlyList<StockBalanceUt>> LoadListAsync(CancellationToken ct = default);
        Task ReloadListAsync(CancellationToken ct = default);
    }
}
