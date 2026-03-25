using Microsoft.EntityFrameworkCore;
using XMS.Application.Abstractions.Services;
using XMS.Core.Abstractions.Data;
using XMS.Domain.Models;
using XMS.Integration.OneC.Api;

namespace XMS.Application.Services
{
    internal class UserUtService(IOneCUtService oneSUtService, IDbContextFactoryProxy dbFactory) : IUserUtService
    {
        public async Task<IReadOnlyList<UserUt>> GetListAsync(bool includeDeleted = false, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var query = dbContext.Set<UserUt>().AsNoTracking();

            if (!includeDeleted)
                query = query.Where(e => !e.IsDeleted);

            return await query.OrderBy(x => x.Name).ToListAsync(ct);
        }

        public async Task<IReadOnlyList<UserUt>> FetchListAsync(CancellationToken ct = default)
        {
            return await oneSUtService.FetchUserUtListAsync(ct);
        }

        public async Task ReloadListAsync(CancellationToken ct = default)
        {
            var list = await FetchListAsync(ct);

            await SaveListAsync(list, ct);
        }

        private async Task SaveListAsync(IReadOnlyList<UserUt> list, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var incomingIds = list.Select(x => x.Id).ToList();

            var existingList = await dbContext.Set<UserUt>().Where(x => incomingIds.Contains(x.Id)).ToListAsync(ct);

            var existingEntities = existingList.ToDictionary(x => x.Id);

            foreach (var incoming in list)
            {
                if (existingEntities.TryGetValue(incoming.Id, out var existing))
                {
                    dbContext.UpdateValues(existing, incoming);
                }
                else
                {
                    dbContext.Set<UserUt>().Add(incoming);
                }
            }

            await dbContext.SaveChangesAsync(ct);
        }
    }
}
