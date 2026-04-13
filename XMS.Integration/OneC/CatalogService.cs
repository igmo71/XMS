using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Core.Common;
using XMS.Integration.OneC.Abstractions;
using XMS.Integration.OneC.Common;
using XMS.Integration.OneC.Ut.ODataClient;

namespace XMS.Integration.OneC;

internal abstract class CatalogService<TEntity>(UtClient utClient, IDbContextFactoryProxy dbFactory, ILogger logger)
    : BaseService, ICatalogService<TEntity>
    where TEntity : class, ICatalog, ISyncable
{
    public async Task<TEntity?> GetAsync(Guid refKey, CancellationToken ct = default)
    {
        using var dbContext = dbFactory.CreateDbContext();

        var result = await dbContext.Set<TEntity>()
            .FindAsync([refKey], cancellationToken: ct);

        return result;
    }

    public async Task<IReadOnlyList<TEntity>> GetListAsync(CatalogQueryParameters parameters, CancellationToken ct = default)
    {
        using var dbContext = dbFactory.CreateDbContext();

        var result = await dbContext.Set<TEntity>()
            .AsNoTracking()
            .HandleCatalogQuery(parameters)
            .ToListAsync(ct);

        return result ?? [];
    }

    public async Task<ServiceResult> ResyncAsync(CancellationToken ct = default)
    {
        using var activity = StartActivity();

        using var dbContext = dbFactory.CreateDbContext();

        await dbContext.Set<TEntity>()
            .ExecuteDeleteAsync(ct);

        var items = await FetchListAsync(ct);

        await dbContext.Set<TEntity>()
            .AddRangeAsync(items, ct);

        await dbContext.SaveChangesAsync(ct);

        return ServiceResult.Success();
    }

    private async Task<IReadOnlyList<TEntity>> FetchListAsync(CancellationToken ct)
    {
        var uri = SyncHelper.GetUri<TEntity>();

        var rootObject = await utClient.GetValueFromJsonAsync<RootObject<TEntity>>(uri, ct);

        var result = rootObject?.Value?.ToList();

        return result ?? [];
    }
}
