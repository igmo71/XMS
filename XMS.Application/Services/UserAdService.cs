using Microsoft.EntityFrameworkCore;
using XMS.Application.Abstractions;
using XMS.Application.Abstractions.Integration;
using XMS.Application.Abstractions.Services;
using XMS.Domain.Models;

namespace XMS.Application.Services
{
    public class UserAdService(IAdService adService, IDbContextFactoryProxy dbFactory) : BaseService, IUserAdService
    {
        public async Task<IReadOnlyList<UserAd>> GetListAsync(CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            return await dbContext.Set<UserAd>()
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<UserAd>> LoadListAsync(CancellationToken ct = default)
        {
            using var activity = StartActivity();

            return await adService.GetUsersAsync(ct);
        }

        public async Task ReloadListAsync(CancellationToken ct = default)
        {
            using var activity = StartActivity();

            var list = await LoadListAsync(ct);

            await SaveListAsync(list, ct);
        }

        public async Task SaveListAsync(IReadOnlyList<UserAd> list, CancellationToken ct = default)
        {
            using var activity = StartActivity();

            using var dbContext = dbFactory.CreateDbContext();

            var incomingIds = list.Select(x => x.Sid).ToList();

            var existingList = await dbContext.Set<UserAd>().Where(x => incomingIds.Contains(x.Sid)).ToListAsync(ct);

            var existingEntities = existingList.ToDictionary(x => x.Sid);

            foreach (var incoming in list)
            {
                if (existingEntities.TryGetValue(incoming.Sid, out var existing))
                {
                    dbContext.UpdateValues(existing, incoming);
                }
                else
                {
                    dbContext.Set<UserAd>().Add(incoming);
                }
            }

            await dbContext.SaveChangesAsync(ct);
        }
    }
}
