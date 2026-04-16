using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Core.Abstractions.EventBus;
using XMS.Core.Common;
using XMS.Integration.OneC.Common;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.ODataClient;
using Dto = XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature.Document_СписаниеБезналичныхДенежныхСредств_Dto;
using Entity = XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature.Document_СписаниеБезналичныхДенежныхСредств;

namespace XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;

internal class Document_СписаниеБезналичныхДенежныхСредств_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    IEventPublisher eventPublisher,
    ILogger<Document_СписаниеБезналичныхДенежныхСредств_NotificationHandler> logger,
    IHostEnvironment hostEnvironment)
    : BaseService, IDocument_СписаниеБезналичныхДенежныхСредств_NotificationHandler
{
    public async Task HandleAsync(DocumentNotification oneCNotifyMessage, CancellationToken ct = default)
    {
        using var activity = StartActivity();

        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - Start {@message}", nameof(HandleAsync), oneCNotifyMessage);

        var fetchedItem = await utClient.FetchByRefKeyAsync<Entity>(oneCNotifyMessage.Ref_Key, ct);

        if (fetchedItem is null)
        {
            if (logger.IsEnabled(LogLevel.Warning))
                logger.LogWarning("{Source} - Failed to feath {@message}", nameof(HandleAsync), oneCNotifyMessage);
            return;
        }

        using var dbContext = dbFactory.CreateDbContext();

        await dbContext.Set<Entity>()
            .Where(e => e.Ref_Key == oneCNotifyMessage.Ref_Key)
            .ExecuteDeleteAsync(ct);

        if (!fetchedItem.DeletionMark && fetchedItem.Posted)
        {
            await dbContext.Set<Entity>().AddAsync(fetchedItem, ct);

            await dbContext.SaveChangesAsync(ct);

            await eventPublisher.PublishAsync(
                IntegrationHelper.GetEventName<Entity>(IntegrationType.Received, hostEnvironment),
                Dto.From(fetchedItem), ct);
        }
        else
        {
            await eventPublisher.PublishAsync(
                IntegrationHelper.GetEventName<Entity>(IntegrationType.Deleted, hostEnvironment),
                Dto.From(fetchedItem), ct);
        }

        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - Ok {@message} {@fetchedItem}", nameof(HandleAsync), oneCNotifyMessage, fetchedItem);
    }
}
