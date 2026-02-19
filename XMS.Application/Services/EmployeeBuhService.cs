using Microsoft.EntityFrameworkCore;
using XMS.Application.Common;
using XMS.Application.Abstractions;
using XMS.Application.Abstractions.Integration;
using XMS.Application.Abstractions.Services;
using XMS.Domain.Models;

namespace XMS.Application.Services
{
    internal class EmployeeBuhService(IOneSBuhService oneSBuhService, IDbContextFactoryProxy dbFactory) : IEmployeeBuhService
    {
        public async Task<IReadOnlyList<EmployeeBuh>> GetListAsync(bool includeDeleted = false, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var query = dbContext.Set<EmployeeBuh>().AsNoTracking();

            if (!includeDeleted)
                query = query.Where(e => !e.IsDeleted);

            return await query.OrderBy(x => x.Name).ToListAsync(ct);
        }

        public async Task<IReadOnlyList<EmployeeBuh>> LoadListAsync(CancellationToken ct = default)
        {
            return await oneSBuhService.GetEmployeeBuhListAsync(ct);
        }

        public async Task ReloadListAsync(CancellationToken ct = default)
        {
            var list = await LoadListAsync(ct);

            await SaveListAsync(list, ct);
        }

        private async Task SaveListAsync(IReadOnlyList<EmployeeBuh> list, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var existingEntities = new Dictionary<Guid, EmployeeBuh>();

            // Avoid translating Contains(...) into OPENJSON/WITH SQL by using batched OR predicates.
            foreach (var batchIds in list.Select(x => x.Id).Distinct().Chunk(200))
            {
                var existingBatch = await dbContext.Set<EmployeeBuh>()
                    .Where(EntityFilterBuilder.BuildIdOrFilter<EmployeeBuh>(batchIds))
                    .ToListAsync(ct);

                foreach (var existing in existingBatch)
                    existingEntities[existing.Id] = existing;
            }

            foreach (var incoming in list)
            {
                if (existingEntities.TryGetValue(incoming.Id, out var existing))
                {
                    dbContext.UpdateValues(existing, incoming);
                }
                else
                {
                    dbContext.Set<EmployeeBuh>().Add(incoming);
                }
            }

            await dbContext.SaveChangesAsync(ct);
        }
    }
}
