using Microsoft.EntityFrameworkCore;
using XMS.Application.Abstractions;
using XMS.Application.Common;
using XMS.Application.Common.Integration;
using XMS.Modules.CostModule.Abstractions;
using XMS.Modules.CostModule.Domain.OneS;

namespace XMS.Modules.CostModule.Application
{
    internal class Document_СписаниеБезналичныхДенежныхСредств_Service(ICostUtService utService, IDbContextFactoryProxy dbFactory)
        : BaseService, IDocument_СписаниеБезналичныхДенежныхСредств_Service
    {
        public async Task<Document_СписаниеБезналичныхДенежныхСредств?> GetAsync(string refKey, CancellationToken ct)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var result = await dbContext.Set<Document_СписаниеБезналичныхДенежныхСредств>().FindAsync(refKey);

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

        public async Task<IReadOnlyList<Document_СписаниеБезналичныхДенежныхСредств>> LoadListAsyncByDate(DateTime date, CancellationToken ct = default)
        {
            var documents = await utService.GetDocument_СписаниеБезналичныхДенежныхСредств_ByDateAsync(date, ct);

            return documents ?? [];
        }

        public Task<ServiceResult> NotifyAsync(OneSNotifyBody notifyBody)
        {
            throw new NotImplementedException();
        }

        public async Task<int> ReloadListAsync(DateTime from, DateTime to, CancellationToken ct = default)
        {
            StartActivity();

            using var dbContext = dbFactory.CreateDbContext();

            await OneSDocumentRepository.DeleteRangeByDateAsync<Document_СписаниеБезналичныхДенежныхСредств>(dbContext, from, to, ct);

            var currentDay = from;

            int insertedRows = 0;

            while (currentDay < to)
            {
                var documents = await LoadListAsyncByDate(currentDay, ct);

                insertedRows += await OneSDocumentRepository.InsertRangeAsync(dbContext, documents, ct);

                currentDay = currentDay.AddDays(1);
            }

            return insertedRows;
        }
    }
}
