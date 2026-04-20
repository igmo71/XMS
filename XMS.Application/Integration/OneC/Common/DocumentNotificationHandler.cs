using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Integration.OneC.Events;

namespace XMS.Application.Integration.OneC.Common;

internal abstract class DocumentNotificationHandler<TEntity>(UtClient utClient, IDbContextFactoryProxy dbFactory, ILogger logger)
    : BaseService, IIntegrationEventHandler<TEntity>
    where TEntity : class, IDocument, ISelectable
{
    public async Task HandleAsync(TEntity oneCNotifyMessage, CancellationToken ct = default)
    {
        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - Start {@message}", nameof(HandleAsync), oneCNotifyMessage);

        await Task.Delay(1000, ct); // TODO: 1С не успевает отпустить документ, когда мы его уже запрашиваем

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

        if (fetchedItem is not null && !fetchedItem.DeletionMark && fetchedItem.Posted)
        {
            await dbContext.Set<TEntity>().AddAsync(fetchedItem, ct);

            await dbContext.SaveChangesAsync(ct);
        }

        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - Ok {@message} {@fetchedItem}", nameof(HandleAsync), oneCNotifyMessage, fetchedItem);
    }
}
