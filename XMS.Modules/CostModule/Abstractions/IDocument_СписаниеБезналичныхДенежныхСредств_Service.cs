using XMS.Application.Common;
using XMS.Modules.CostModule.Domain.OneS;

namespace XMS.Modules.CostModule.Abstractions
{
    public interface IDocument_СписаниеБезналичныхДенежныхСредств_Service
    {
        Task<ServiceResult> CreateOrUpdateAsync(string ref_Key);
        Task<ServiceResult> DeleteAsync(string ref_Key);

        Task<Document_СписаниеБезналичныхДенежныхСредств?> GetAsync(string refKey, CancellationToken ct);

        /// <summary>
        /// Get Document_СписаниеБезналичныхДенежныхСредств from DB by DocumentQueryParameters
        /// Получить документы СписаниеБезналичныхДенежныхСредств из базы данных по поисковым параметрам
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IReadOnlyList<Document_СписаниеБезналичныхДенежныхСредств>> GetListAsync(DocumentQueryParameters parameters, CancellationToken ct = default);

        /// <summary>
        /// Load Document_СписаниеБезналичныхДенежныхСредств from OneS Ut
        /// Загрузить документы СписаниеБезналичныхДенежныхСредств за определенную дату из 1С УТ.
        /// </summary>
        /// <param name="documentService"></param>
        /// <param name="date"></param>
        Task<IReadOnlyList<Document_СписаниеБезналичныхДенежныхСредств>> LoadListAsyncByDate(DateTime date, CancellationToken ct = default);

        /// <summary>
        /// Reload Document_СписаниеБезналичныхДенежныхСредств from OneS Ut
        /// Загрузить документы СписаниеБезналичныхДенежныхСредств за определенный период из 1С УТ и записать их БД
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<int> ReloadListAsync(DateTime from, DateTime to, CancellationToken ct = default);
    }
}
