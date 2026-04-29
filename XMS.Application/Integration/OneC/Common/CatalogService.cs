using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Data;
using XMS.Integrations.OneC;
using XMS.Application.Common;
using XMS.Integrations.OneC.Ut.ODataClient;

namespace XMS.Integrations.OneC.Common;

internal abstract class CatalogService<TEntity>(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger logger)
    : BaseService, ICatalogService<TEntity>
    where TEntity : Catalog
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
        var uri = IntegrationHelper.GetUri<TEntity>();

        var rootObject = await utClient.GetValueFromJsonAsync<RootObject<TEntity>>(uri, ct);

        var result = rootObject?.Value?.ToList();

        return result ?? [];
    }
}
