using Microsoft.EntityFrameworkCore;
using XMS.Application.Abstractions;
using XMS.Core.Abstractions.Data;
using XMS.Modules.CostModule.Abstractions;
using XMS.Modules.CostModule.Domain;

namespace XMS.Modules.CostModule.Application
{
    /// <summary>
    /// Catalog_СтатьиДвиженияДенежныхСредств Service
    /// </summary>
    /// <param name="oneSUtService"></param>
    /// <param name="dbFactory"></param>
    internal class CashFlowItemService(ICostUtService utService, IDbContextFactoryProxy dbFactory) : ICashFlowItemService
    {
        public async Task<IReadOnlyList<CashFlowItem>> GetListAsync(bool includeDeleted = false, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var query = dbContext.Set<CashFlowItem>().AsNoTracking();

            if (!includeDeleted)
                query = query.Where(e => !e.IsDeleted);

            return await query.OrderBy(x => x.Name).ToListAsync(ct);
        }

        public async Task<IReadOnlyList<CashFlowItem>> LoadListAsync(CancellationToken ct = default)
        {
            return await utService.GetCashFlowItemListAsync(ct);
        }

        public async Task ReloadListAsync(CancellationToken ct = default)
        {
            var list = await LoadListAsync(ct);

            await SaveListAsync(list, ct);
        }

        private async Task SaveListAsync(IReadOnlyList<CashFlowItem> list, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var incomingIds = list.Select(x => x.Id).ToList();

            var existingList = await dbContext.Set<CashFlowItem>().Where(x => incomingIds.Contains(x.Id)).ToListAsync(ct);

            var existingEntities = existingList.ToDictionary(x => x.Id);

            foreach (var incoming in list)
            {
                if (existingEntities.TryGetValue(incoming.Id, out var existing))
                {
                    dbContext.UpdateValues(existing, incoming);
                }
                else
                {
                    dbContext.Set<CashFlowItem>().Add(incoming);
                }
            }

            await dbContext.SaveChangesAsync(ct);
        }
    }
}
