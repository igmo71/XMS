using Microsoft.EntityFrameworkCore;
using XMS.Application.Abstractions;
using XMS.Application.Abstractions.Integration;
using XMS.Application.Abstractions.Services;
using XMS.Domain.Models;

namespace XMS.Application.Services
{
    internal class CashFlowItemService(IOneSUtService oneSUtService, IDbContextFactoryProxy dbFactory) : ICashFlowItemService
    {
        public async Task<IReadOnlyList<CashFlowItem>> GetListAsync(bool ignoreQueryFilters = false, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var query = dbContext.Set<CashFlowItem>().AsNoTracking();

            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();

            return await query.OrderBy(x => x.Name).ToListAsync(ct);
        }

        public async Task<IReadOnlyList<CashFlowItem>> LoadListAsync(CancellationToken ct = default)
        {
            return await oneSUtService.GetCashFlowItemListAsync(ct);
        }

        public async Task ReloadListAsync(CancellationToken ct = default)
        {
            var list = await LoadListAsync(ct);

            await SaveListAsync(list, ct);
        }

        public async Task SaveListAsync(IReadOnlyList<CashFlowItem> list, CancellationToken ct = default)
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
