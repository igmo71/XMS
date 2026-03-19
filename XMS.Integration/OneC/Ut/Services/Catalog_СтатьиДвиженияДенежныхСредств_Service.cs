using Microsoft.EntityFrameworkCore;
using XMS.Core.Abstractions.Data;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.Models;

namespace XMS.Integration.OneC.Ut.Services
{
    internal class Catalog_СтатьиДвиженияДенежныхСредств_Service(IDbContextFactoryProxy dbFactory) : ICatalog_СтатьиДвиженияДенежныхСредств_Service
    {
        public Task<IReadOnlyList<Catalog_СтатьиДвиженияДенежныхСредств>> FetchListAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Catalog_СтатьиДвиженияДенежныхСредств>> GetListAsync(bool includeDeleted = false, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var query = dbContext.Set<Catalog_СтатьиДвиженияДенежныхСредств>().AsNoTracking();

            if (!includeDeleted)
                query = query.Where(e => !e.DeletionMark);

            return await query.OrderBy(x => x.Description).ToListAsync(ct);
        }

        public Task ResyncListAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
