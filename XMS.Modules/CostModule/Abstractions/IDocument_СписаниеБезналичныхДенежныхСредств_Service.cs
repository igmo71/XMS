using XMS.Application.Common;
using XMS.Modules.CostModule.Domain.OneS;

namespace XMS.Modules.CostModule.Abstractions
{
    internal interface IDocument_СписаниеБезналичныхДенежныхСредств_Service
    {
        Task<IReadOnlyList<Document_СписаниеБезналичныхДенежныхСредств>> GetListAsync(DocumentQueryParameters parameters, CancellationToken ct = default);

        Task<IReadOnlyList<Document_СписаниеБезналичныхДенежныхСредств>> LoadListAsyncByDate(DateTime date, CancellationToken ct = default);

        Task<int> ReloadListAsync(DateTime from, DateTime to, CancellationToken ct = default);
    }
}
