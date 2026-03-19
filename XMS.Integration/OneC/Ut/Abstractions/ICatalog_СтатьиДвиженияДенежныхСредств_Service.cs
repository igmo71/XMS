using XMS.Integration.OneC.Ut.Models;

namespace XMS.Integration.OneC.Ut.Abstractions
{
    internal interface ICatalog_СтатьиДвиженияДенежныхСредств_Service
    {
        Task<IReadOnlyList<Catalog_СтатьиДвиженияДенежныхСредств>> FetchListAsync(CancellationToken ct = default);
        Task<IReadOnlyList<Catalog_СтатьиДвиженияДенежныхСредств>> GetListAsync(bool includeDeleted = false, CancellationToken ct = default);
        Task ResyncListAsync(CancellationToken ct = default);
    }
}
