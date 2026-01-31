using Microsoft.EntityFrameworkCore;
using XMS.Data;
using XMS.Integration.OneS.Abstractions;
using XMS.Modules.Employees.Abstractions;
using XMS.Modules.Employees.Domain;

namespace XMS.Modules.Employees.Application
{
    public class UserUtService(IUtService utService, IDbContextFactory<ApplicationDbContext> dbFactory) : IUserUtService
    {
        public async Task<IReadOnlyList<UserUt>> GetListAsync(CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            return await dbContext.UsersUt
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<UserUt>> LoadListAsync(CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            var rawData = await utService.GetCatalog_Пользователи(ct);

            return rawData.Select(x => new UserUt
            {
                Id = x.Ref_Key,
                Name = x.Description ?? string.Empty,
                DeletionMark = x.DeletionMark
            }).ToList();
        }

        public async Task ReloadListAsync(CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            var list = await LoadListAsync(ct);
            await SaveListAsync(list, ct);
        }

        public async Task SaveListAsync(IReadOnlyList<UserUt> list, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var incomingIds = list.Select(x => x.Id).ToList();

            var existingList = await dbContext.UsersUt.Where(x => incomingIds.Contains(x.Id)).ToListAsync(ct);

            var existingEntities = existingList.ToDictionary(x => x.Id);

            foreach (var incoming in list)
            {
                if (existingEntities.TryGetValue(incoming.Id, out var existing))
                {
                    dbContext.Entry(existing).CurrentValues.SetValues(incoming);
                }
                else
                {
                    dbContext.UsersUt.Add(incoming);
                }
            }

            await dbContext.SaveChangesAsync(ct);
        }
    }
}
