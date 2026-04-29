using XMS.Application.Common;
using XMS.Integrations.OneC.Common;
using XMS.Integrations.OneC.Ut.Features.Catalog_Пользователи_Feature;
using XMS.Integrations.OneC.Ut.Features.Catalog_СтатьиДвиженияДенежныхСредств_Feature;
using XMS.Integrations.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature;
using XMS.Integrations.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;
using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Integration;

public interface IOneCUtService
{
    Task<IReadOnlyList<UserUt>> FetchUserUtListAsync(CancellationToken ct = default);

    //Task<IReadOnlyList<AccumulationRegister_ТоварыНаСкладах_Balance>> GetAccumulationRegister_ТоварыНаСкладах_Balance_Async(CancellationToken ct = default);

    Task<IReadOnlyList<Catalog_Пользователи>> GetCatalog_Пользователи_Async(CatalogQueryParameters parameters, CancellationToken ct = default);
    Task<Catalog_Пользователи?> GetCatalog_Пользователи_Async(Guid refKey, CancellationToken ct = default);
    Task<ServiceResult> ResyncCatalog_Пользователи_Async(CancellationToken ct = default);

    Task<IReadOnlyList<Catalog_СтатьиДвиженияДенежныхСредств>> GetCatalog_СтатьиДвиженияДенежныхСредств_Async(CatalogQueryParameters parameters, CancellationToken ct = default);
    Task<Catalog_СтатьиДвиженияДенежныхСредств?> GetCatalog_СтатьиДвиженияДенежныхСредств_Async(Guid refKey, CancellationToken ct = default);
    Task<ServiceResult> ResyncCatalog_СтатьиДвиженияДенежныхСредств_Async(CancellationToken ct = default);

    Task<IReadOnlyList<Document_РасходныйКассовыйОрдер>> GetDocument_РасходныйКассовыйОрдер_Async(DocumentQueryParameters parameters, CancellationToken ct = default);
    Task<Document_РасходныйКассовыйОрдер?> GetDocument_РасходныйКассовыйОрдер_Async(Guid refKey, CancellationToken ct = default);

    Task<IReadOnlyList<Document_СписаниеБезналичныхДенежныхСредств>> GetDocument_СписаниеБезналичныхДенежныхСредств_Async(DocumentQueryParameters parameters, CancellationToken ct = default);
    Task<Document_СписаниеБезналичныхДенежныхСредств?> GetDocument_СписаниеБезналичныхДенежныхСредств_Async(Guid refKey, CancellationToken ct = default);
}
