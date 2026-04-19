using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Data;
using XMS.Application.Core.Common;
using XMS.Application.Abstractions.Integration.Events;
using XMS.Application.Abstractions.Integration.OneC;
using XMS.Application.Integration.OneC.Ut.ODataClient;

namespace XMS.Application.Integration.OneC.Common;

internal abstract class CatalogNotificationHandler<TEntity, TEvent>(UtClient utClient, IDbContextFactoryProxy dbFactory, ILogger logger)
    : BaseService, IIntegrationEventHandler<TEvent>
    where TEntity : class, ICatalog, ISelectable
    where TEvent : class, IIntegrationEvent
{
    public async Task HandleAsync(TEvent oneCNotifyMessage, CancellationToken ct = default)
    {
        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - Start {@message}", nameof(HandleAsync), oneCNotifyMessage);

        using var activity = StartActivity();

        var fetchedItem = await utClient.FetchByRefKeyAsync<TEntity>(oneCNotifyMessage.Ref_Key, ct);

        if (fetchedItem is null)
        {
            if (logger.IsEnabled(LogLevel.Warning))
                logger.LogWarning("{Source} - Failed to fetch {@message}", nameof(HandleAsync), oneCNotifyMessage);
            return;
        }

        using var dbContext = dbFactory.CreateDbContext();

        await dbContext.Set<TEntity>()
            .Where(e => e.Ref_Key == oneCNotifyMessage.Ref_Key)
            .ExecuteDeleteAsync(ct);

        await dbContext.Set<TEntity>().AddAsync(fetchedItem, ct);

        await dbContext.SaveChangesAsync(ct);

        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - Ok {@message} {@fetchedItem}", nameof(HandleAsync), oneCNotifyMessage, fetchedItem);
    }
}
