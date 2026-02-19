using Microsoft.EntityFrameworkCore;
using XMS.Application.Common;
using XMS.Application.Abstractions;
using XMS.Application.Abstractions.Integration;
using XMS.Application.Abstractions.Services;
using XMS.Domain.Models;

namespace XMS.Application.Services
{
    internal class UserAdService(IAdService adService, IDbContextFactoryProxy dbFactory) : BaseService, IUserAdService
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

        private async Task SaveListAsync(IReadOnlyList<UserAd> list, CancellationToken ct = default)
        {
            using var activity = StartActivity();

            using var dbContext = dbFactory.CreateDbContext();

            var existingEntities = new Dictionary<string, UserAd>(StringComparer.Ordinal);

            // Avoid translating Contains(...) into OPENJSON/WITH SQL by using batched OR predicates.
            foreach (var batchIds in list.Select(x => x.Sid).Distinct(StringComparer.Ordinal).Chunk(200))
            {
                var existingBatch = await dbContext.Set<UserAd>()
                    .Where(EntityFilterBuilder.BuildStringOrFilter<UserAd>(nameof(UserAd.Sid), batchIds))
                    .ToListAsync(ct);

                foreach (var existing in existingBatch)
                    existingEntities[existing.Sid] = existing;
            }

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
