using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Core.Common;
using XMS.Integration.OneC.Ut.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature
{
    internal class Document_СписаниеБезналичныхДенежныхСредств_Service(
        UtClient utClient,
        IDbContextFactoryProxy dbFactory,
        ILogger<Document_СписаниеБезналичныхДенежныхСредств_Service> logger)
        : BaseService, IDocument_СписаниеБезналичныхДенежныхСредств_Service
    {
        public async Task<ServiceResult> HandleEventOneC(Document_СписаниеБезналичныхДенежныхСредств_Changed oneCNotifyMessage, CancellationToken ct = default)
        {
            logger.LogDebug("{Source} - Start {@message}", nameof(HandleEventOneC), oneCNotifyMessage);

            var fetchedItem = await FetchByRefKeyAsync(oneCNotifyMessage.Ref_Key, ct);

            if (fetchedItem is null)
            {
                logger.LogError("{Source} - Failed to feath {@message}", nameof(HandleEventOneC), oneCNotifyMessage);
                return ServiceError.NotFound;
            }

            using var dbContext = dbFactory.CreateDbContext();

            await dbContext.Set<Document_СписаниеБезналичныхДенежныхСредств>()
                .Where(e => e.Ref_Key == oneCNotifyMessage.Ref_Key)
                .ExecuteDeleteAsync(ct);

            if (!fetchedItem.DeletionMark && fetchedItem.Posted)
            {
                await dbContext.Set<Document_СписаниеБезналичныхДенежныхСредств>()
                .AddAsync(fetchedItem, ct);

                await dbContext.SaveChangesAsync(ct);
            }

            logger.LogDebug("{Source} - Ok {@message} {@fetchedItem}", nameof(HandleEventOneC), oneCNotifyMessage, fetchedItem);

            return ServiceResult.Success();
        }

        public async Task<Document_СписаниеБезналичныхДенежныхСредств?> GetAsync(Guid refKey, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var result = await dbContext.Set<Document_СписаниеБезналичныхДенежныхСредств>()
                .FindAsync([refKey], cancellationToken: ct);

            return result;
        }

        public async Task<IReadOnlyList<Document_СписаниеБезналичныхДенежныхСредств>> GetListAsync(DocumentQueryParameters parameters, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var result = await dbContext.Set<Document_СписаниеБезналичныхДенежныхСредств>()
                .AsNoTracking()
                .HandleDocumentQuery(parameters)
                .ToListAsync(ct);

            return result ?? [];
        }

        public async Task<ServiceResult> ResyncAsync(DateTime from, DateTime to, CancellationToken ct = default)
        {
            StartActivity();

            using var dbContext = dbFactory.CreateDbContext();

            await dbContext.Set<Document_СписаниеБезналичныхДенежныхСредств>()
                .Where(e => e.Date >= from && e.Date < to)
                .ExecuteDeleteAsync(ct);

            var currentDay = from;

            while (currentDay < to)
            {
                var items = await FetchListAsyncByDate(currentDay, ct);

                await dbContext.Set<Document_СписаниеБезналичныхДенежныхСредств>()
                    .AddRangeAsync(items, ct);

                await dbContext.SaveChangesAsync(ct);

                currentDay = currentDay.AddDays(1);
            }

            return ServiceResult.Success();
        }

        private async Task<Document_СписаниеБезналичныхДенежныхСредств?> FetchByRefKeyAsync(Guid refKey, CancellationToken ct = default)
        {
            var uri = Document_СписаниеБезналичныхДенежныхСредств.GetUriByRefKey(refKey);

            var rootObject = await utClient.GetValueFromJsonAsync<RootObject<Document_СписаниеБезналичныхДенежныхСредств>>(uri, ct);

            var result = rootObject?.Value?.FirstOrDefault();

            return result;
        }

        private async Task<IReadOnlyList<Document_СписаниеБезналичныхДенежныхСредств>> FetchListAsyncByDate(DateTime date, CancellationToken ct = default)
        {
            var uri = Document_СписаниеБезналичныхДенежныхСредств.GetUriByDate(date, date.AddDays(1));

            var rootObject = await utClient.GetValueAsync<RootObject<Document_СписаниеБезналичныхДенежныхСредств>>(uri, ct);

            var result = rootObject?.Value?.ToList();

            return result ?? [];
        }
    }
}
