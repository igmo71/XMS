using Microsoft.EntityFrameworkCore;
using XMS.Data;
using XMS.Integration.OneS.Abstractions;
using XMS.Modules.Employees.Abstractions;
using XMS.Modules.Employees.Domain;

namespace XMS.Modules.Employees.Application
{
    public class EmployeeZupService(IZupService zupService, IDbContextFactory<ApplicationDbContext> dbFactory) : IEmployeeZupService
    {
        public async Task<IReadOnlyList<EmployeeZup>> GetListAsync(CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            return await dbContext.EmployeesZup
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<EmployeeZup>> LoadListAsync(CancellationToken ct = default)
        {
            var rawData = await zupService.GetCatalog_Сотрудники(ct);

            return rawData.Select(x => new EmployeeZup
            {
                Id = x.Ref_Key,
                Name = x.Description ?? string.Empty,
                DeletionMark = x.DeletionMark,
                Code = x.Code,
                Archived = x.ВАрхиве
            }).ToList();
        }

        public async Task ReloadListAsync(CancellationToken ct = default)
        {
            var list = await LoadListAsync(ct);
            await SaveListAsync(list, ct);
        }

        public async Task SaveListAsync(IReadOnlyList<EmployeeZup> list, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var incomingIds = list.Select(x => x.Id).ToList();

            var existingList = await dbContext.EmployeesZup.Where(x => incomingIds.Contains(x.Id)).ToListAsync(ct);

            var existingEntities = existingList.ToDictionary(x => x.Id);

            foreach (var incoming in list)
            {
                if (existingEntities.TryGetValue(incoming.Id, out var existing))
                {
                    dbContext.Entry(existing).CurrentValues.SetValues(incoming);
                }
                else
                {
                    dbContext.EmployeesZup.Add(incoming);
                }
            }

            await dbContext.SaveChangesAsync(ct);
        }
    }
}
