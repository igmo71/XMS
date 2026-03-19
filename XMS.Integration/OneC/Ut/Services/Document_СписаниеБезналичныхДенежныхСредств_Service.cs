using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Core.Common;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.Models;

namespace XMS.Integration.OneC.Ut.Services
{
    internal class Document_СписаниеБезналичныхДенежныхСредств_Service(
        UtClient utClient,
        IDbContextFactoryProxy dbFactory,
        ILogger<Document_СписаниеБезналичныхДенежныхСредств_Service> logger)
        : BaseService, IDocument_СписаниеБезналичныхДенежныхСредств_Service
    {
        public async Task<ServiceResult> CreateOrUpdateAsync(Guid refKey, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var newDocument = await FetchByRefKeyAsync(refKey, ct);

            if (newDocument is null)
                return ServiceError.InvalidOperation.WithDescription($"Failed to feath Document_СписаниеБезналичныхДенежныхСредств by {refKey}");

            await dbContext.Set<Document_СписаниеБезналичныхДенежныхСредств>()
                .Where(e => e.Ref_Key == refKey)
                .ExecuteDeleteAsync();

            await dbContext.Set<Document_СписаниеБезналичныхДенежныхСредств>()
                .AddAsync(newDocument);

            await dbContext.SaveChangesAsync(ct);

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> DeleteAsync(Guid refKey, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            await dbContext.Set<Document_СписаниеБезналичныхДенежныхСредств>()
                .Where(e => e.Ref_Key == refKey)
                .ExecuteDeleteAsync();

            logger.LogDebug("{Source} {refKey}", nameof(DeleteAsync), refKey);

            return ServiceResult.Success();
        }

        public async Task<Document_СписаниеБезналичныхДенежныхСредств?> GetAsync(Guid refKey, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var result = await dbContext.Set<Document_СписаниеБезналичныхДенежныхСредств>().FindAsync(new object?[] { refKey }, cancellationToken: ct);

            return result;
        }

        public async Task<IReadOnlyList<Document_СписаниеБезналичныхДенежныхСредств>> GetListAsync(DocumentQueryParameters parameters, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var result = await dbContext.Set<Document_СписаниеБезналичныхДенежныхСредств>()
                .AsNoTracking()
                .HandleDocumentQueryParameters(parameters)
                .ToListAsync(ct);

            return result;
        }

        private async Task<Document_СписаниеБезналичныхДенежныхСредств?> FetchByRefKeyAsync(Guid refKey, CancellationToken ct = default)
        {
            var uri = Document_СписаниеБезналичныхДенежныхСредств.GetUriByRefKey(refKey);

            var rootObject = await utClient.GetValueAsync<RootObject<Document_СписаниеБезналичныхДенежныхСредств>>(uri, ct);

            var result = rootObject?.Value?[0];

            return result;
        }


        private async Task<IReadOnlyList<Document_СписаниеБезналичныхДенежныхСредств>> FetchListAsyncByDate(DateTime date, CancellationToken ct = default)
        {
            var uri = Document_СписаниеБезналичныхДенежныхСредств.GetUriByDate(date, date.AddDays(1));

            var rootObject = await utClient.GetValueAsync<RootObject<Document_СписаниеБезналичныхДенежныхСредств>>(uri, ct);

            var result = rootObject?.Value?.ToList();

            return result ?? [];
        }


        public async Task<int> ResyncByDateRangeAsync(DateTime from, DateTime to, CancellationToken ct = default)
        {
            StartActivity();

            using var dbContext = dbFactory.CreateDbContext();

            await DocumentRepository.DeleteRangeByDateAsync<Document_СписаниеБезналичныхДенежныхСредств>(dbContext, from, to, ct);

            var currentDay = from;

            int insertedRows = 0;

            while (currentDay < to)
            {
                var documents = await FetchListAsyncByDate(currentDay, ct);

                insertedRows += await DocumentRepository.InsertRangeAsync(dbContext, documents, ct);

                currentDay = currentDay.AddDays(1);
            }

            return insertedRows;
        }
    }
}
