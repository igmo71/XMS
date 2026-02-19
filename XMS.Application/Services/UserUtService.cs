using Microsoft.EntityFrameworkCore;
using XMS.Application.Abstractions;
using XMS.Application.Abstractions.Integration;
using XMS.Application.Abstractions.Services;
using XMS.Domain.Models;

namespace XMS.Application.Services
{
    internal class UserUtService(IOneSUtService oneSUtService, IDbContextFactoryProxy dbFactory) : IUserUtService
    {
        public async Task<IReadOnlyList<UserUt>> GetListAsync(bool includeDeleted = false, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var query = dbContext.Set<UserUt>().AsNoTracking();

            if (!includeDeleted)
                query = query.Where(e => !e.IsDeleted);

            return await query.OrderBy(x => x.Name).ToListAsync(ct);
        }

        public async Task<IReadOnlyList<UserUt>> LoadListAsync(CancellationToken ct = default)
        {
            return await oneSUtService.GetUserUtListAsync(ct);
        }

        public async Task ReloadListAsync(CancellationToken ct = default)
        {
            var list = await LoadListAsync(ct);

            await SaveListAsync(list, ct);
        }

        private async Task SaveListAsync(IReadOnlyList<UserUt> list, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            // NOTE: Avoid translating Contains(...) into OPENJSON/WITH SQL for older SQL Server compatibility.
            var existingEntities = await dbContext.Set<UserUt>()
                .ToDictionaryAsync(x => x.Id, ct);

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
