using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Data;
using XMS.Application.Abstractions.Integration.OneC;
using XMS.Application.Common;
using XMS.Application.Integration.OneC.Ut.ODataClient;

namespace XMS.Application.Integration.OneC.Common;

internal abstract class DocumentService<TEntity>(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    ILogger logger)
    : BaseService, IDocumentService<TEntity>
    where TEntity : Document
{
    public async Task<TEntity?> GetAsync(Guid refKey, CancellationToken ct = default)
    {
        using var dbContext = dbFactory.CreateDbContext();

        var result = await dbContext.Set<TEntity>()
            .FindAsync([refKey], cancellationToken: ct);

        return result;
    }

    public async Task<IReadOnlyList<TEntity>> GetListAsync(DocumentQueryParameters parameters, CancellationToken ct = default)
    {
        using var dbContext = dbFactory.CreateDbContext();

        var result = await dbContext.Set<TEntity>()
            .AsNoTracking()
            .HandleDocumentQuery(parameters)
            .ToListAsync(ct);

        return result ?? [];
    }

    public async Task<ServiceResult> ResyncAsync(DateTime from, DateTime to, CancellationToken ct = default)
    {
        using var activity = StartActivity();

        using var dbContext = dbFactory.CreateDbContext();

        await dbContext.Set<TEntity>()
            .Where(e => e.Date >= from && e.Date < to)
            .ExecuteDeleteAsync(ct);

        var currentDay = from;

        while (currentDay < to)
        {
            var items = await FetchListAsyncByDate(currentDay, ct);

            await dbContext.Set<TEntity>()
                .AddRangeAsync(items, ct);

            await dbContext.SaveChangesAsync(ct);

            currentDay = currentDay.AddDays(1);
        }

        return ServiceResult.Success();
    }

    private async Task<IReadOnlyList<TEntity>> FetchListAsyncByDate(DateTime date, CancellationToken ct)
    {
        var uri = IntegrationHelper.GetUriByDate<TEntity>(date, date.AddDays(1));

        var rootObject = await utClient.GetValueFromJsonAsync<RootObject<TEntity>>(uri, ct);

        var result = rootObject?.Value?.ToList();

        return result ?? [];
    }
}
