using XMS.Core.Common;
using XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;

namespace XMS.Integration.OneC.Ut.Abstractions
{
    public interface IDocument_СписаниеБезналичныхДенежныхСредств_Service
    {
        Task<ServiceResult> HandleEventOneC(Document_СписаниеБезналичныхДенежныхСредств_Changed oneCNotifyMessage, CancellationToken ct = default);

        Task<Document_СписаниеБезналичныхДенежныхСредств?> GetAsync(Guid refKey, CancellationToken ct = default);

        Task<IReadOnlyList<Document_СписаниеБезналичныхДенежныхСредств>> GetListAsync(DocumentQueryParameters parameters, CancellationToken ct = default);

        Task<ServiceResult> ResyncByDateRangeAsync(DateTime from, DateTime to, CancellationToken ct = default);
    }
}
