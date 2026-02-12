using Microsoft.EntityFrameworkCore;
using XMS.Web.Core.Abstractions;
using XMS.Web.Data;
using XMS.Web.Integration.AD.Application;
using XMS.Web.Modules.Employees.Abstractions;
using XMS.Web.Modules.Employees.Domain;

namespace XMS.Web.Modules.Employees.Application
{
    public class UserAdService(IAdService adService, IDbContextFactory<ApplicationDbContext> dbFactory) : BaseService, IUserAdService
    {
        public async Task<IReadOnlyList<UserAd>> GetListAsync(CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            return await dbContext.UsersAd
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<UserAd>> LoadListAsync(CancellationToken ct = default)
        {
            using var activity = StartActivity();

            var rawData = await adService.GetUsersAsync(ct);

            return rawData.Select(x => new UserAd
            {
                Sid = x.Sid ?? string.Empty,
                Name = x.Name ?? string.Empty,
                Login = x.Login,
                Title = x.Title,
                Department = x.Department,
                Manager = x.Manager,
                DistinguishedName = x.DistinguishedName,
                Enabled = x.Enabled
            }).ToList();
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

            var existingList = await dbContext.UsersAd.Where(x => incomingIds.Contains(x.Sid)).ToListAsync(ct);

            var existingEntities = existingList.ToDictionary(x => x.Sid);

            foreach (var incoming in list)
            {
                if (existingEntities.TryGetValue(incoming.Sid, out var existing))
                {
                    dbContext.Entry(existing).CurrentValues.SetValues(incoming);
                }
                else
                {
                    dbContext.UsersAd.Add(incoming);
                }
            }

            await dbContext.SaveChangesAsync(ct);
        }
    }
}
