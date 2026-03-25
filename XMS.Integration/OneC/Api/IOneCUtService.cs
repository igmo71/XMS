using XMS.Core.Common;
using XMS.Domain.Models;
using XMS.Integration.OneC.Ut.Features.AccumulationRegister_ТоварыНаСкладах_Balance_Feature;
using XMS.Integration.OneC.Ut.Features.Catalog_СтатьиДвиженияДенежныхСредств_Feature;
using XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;

namespace XMS.Integration.OneC.Api;

public interface IOneCUtService
{
    Task<IReadOnlyList<UserUt>> FetchUserUtListAsync(CancellationToken ct = default);

    Task<IReadOnlyList<AccumulationRegister_ТоварыНаСкладах_Balance>> GetAccumulationRegister_ТоварыНаСкладах_Balance_Async(CancellationToken ct = default);

    Task<IReadOnlyList<Catalog_СтатьиДвиженияДенежныхСредств>> GetCatalog_СтатьиДвиженияДенежныхСредств_Async(CatalogQueryParameters parameters, CancellationToken ct = default);
    Task<Catalog_СтатьиДвиженияДенежныхСредств?> GetCatalog_СтатьиДвиженияДенежныхСредств_Async(Guid refKey, CancellationToken ct = default);
    Task<ServiceResult> ResyncAsync(CancellationToken ct = default);

    Task<IReadOnlyList<Document_СписаниеБезналичныхДенежныхСредств>> GetDocument_СписаниеБезналичныхДенежныхСредств_Async(DocumentQueryParameters parameters, CancellationToken ct = default);
    Task<Document_СписаниеБезналичныхДенежныхСредств?> GetDocument_СписаниеБезналичныхДенежныхСредств_Async(Guid refKey, CancellationToken ct = default);
}
