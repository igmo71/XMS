using Microsoft.EntityFrameworkCore;
using XMS.Application.Abstractions;
using XMS.Application.Abstractions.Integration;
using XMS.Application.Abstractions.Services;
using XMS.Domain.Models;

namespace XMS.Application.Services
{
    public class UserUtService(IOneSUtService oneSUtService, IDbContextFactoryProxy dbFactory) : IUserUtService
    {
        public async Task<IReadOnlyList<UserUt>> GetListAsync(CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            return await dbContext.Set<UserUt>()
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync(ct);
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

        public async Task SaveListAsync(IReadOnlyList<UserUt> list, CancellationToken ct = default)
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
