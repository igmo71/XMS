using Microsoft.EntityFrameworkCore;
using XMS.Application.Common;
using XMS.Application.Abstractions;
using XMS.Application.Abstractions.Integration;
using XMS.Application.Abstractions.Services;
using XMS.Domain.Models;

namespace XMS.Application.Services
{
    internal class StockBalanceUtService(IOneSUtService oneSUtService, IDbContextFactoryProxy dbFactory) : IStockBalanceUtService
    {
        public async Task<IReadOnlyList<StockBalanceUt>> GetListAsync(CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            return await dbContext.Set<StockBalanceUt>()
                .AsNoTracking()
                .OrderBy(x => x.NomenclatureId)
                .ThenBy(x => x.WarehouseId)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<StockBalanceUt>> LoadListAsync(CancellationToken ct = default)
        {
            return await oneSUtService.GetStockBalanceUtListAsync(ct);
        }

        public async Task ReloadListAsync(CancellationToken ct = default)
        {
            var list = await LoadListAsync(ct);

            await SaveListAsync(list, ct);
        }

        private async Task SaveListAsync(IReadOnlyList<StockBalanceUt> list, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var existingEntities = new Dictionary<Guid, StockBalanceUt>();

            // Avoid translating Contains(...) into OPENJSON/WITH SQL by using batched OR predicates.
            foreach (var batchIds in list.Select(x => x.Id).Distinct().Chunk(200))
            {
                var existingBatch = await dbContext.Set<StockBalanceUt>()
                    .Where(EntityFilterBuilder.BuildIdOrFilter<StockBalanceUt>(batchIds))
                    .ToListAsync(ct);

                foreach (var existing in existingBatch)
                    existingEntities[existing.Id] = existing;
            }

            foreach (var incoming in list)
            {
                if (existingEntities.TryGetValue(incoming.Id, out var existing))
                {
                    dbContext.UpdateValues(existing, incoming);
                }
                else
                {
                    dbContext.Set<StockBalanceUt>().Add(incoming);
                }
            }

            await dbContext.SaveChangesAsync(ct);
        }
    }
}
