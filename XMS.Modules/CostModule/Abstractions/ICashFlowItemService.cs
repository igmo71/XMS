namespace XMS.Modules.CostModule.Abstractions
{
    /// <summary>
    /// Catalog_СтатьиДвиженияДенежныхСредств Service
    /// </summary>
    /// <param name="oneSUtService"></param>
    /// <param name="dbFactory"></param>
    public interface ICashFlowItemService
    {
        Task<IReadOnlyList<CashFlowItem>> GetListAsync(bool includeDeleted = false, CancellationToken ct = default);
        Task<IReadOnlyList<CashFlowItem>> LoadListAsync(CancellationToken ct = default);
        Task ReloadListAsync(CancellationToken ct = default);
    }
}
