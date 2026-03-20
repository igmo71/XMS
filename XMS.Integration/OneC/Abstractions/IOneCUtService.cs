using XMS.Domain.Models;
using XMS.Integration.OneC.Ut.Models;

namespace XMS.Integration.OneC.Abstractions
{
    public interface IOneCUtService
    {
        Task<IReadOnlyList<UserUt>> FetchUserUtListAsync(CancellationToken ct = default);

        Task<IReadOnlyList<AccumulationRegister_ТоварыНаСкладах_Balance>> GetAccumulationRegister_ТоварыНаСкладах_Balance_Async(CancellationToken ct = default);

        Task<IReadOnlyList<Catalog_СтатьиДвиженияДенежныхСредств>> GetCatalog_СтатьиДвиженияДенежныхСредств_Async(CatalogQueryParameters parameters, CancellationToken ct = default);
        Task<Catalog_СтатьиДвиженияДенежныхСредств?> GetCatalog_СтатьиДвиженияДенежныхСредств_Async(Guid refKey, CancellationToken ct = default);

        Task<IReadOnlyList<Document_СписаниеБезналичныхДенежныхСредств>> GetDocument_СписаниеБезналичныхДенежныхСредств_Async(DocumentQueryParameters parameters, CancellationToken ct = default);
        Task<Document_СписаниеБезналичныхДенежныхСредств?> GetDocument_СписаниеБезналичныхДенежныхСредств_Async(Guid refKey, CancellationToken ct = default);
    }
}
