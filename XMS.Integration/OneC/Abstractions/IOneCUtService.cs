namespace XMS.Integration.OneC.Abstractions
{
    public interface IOneCUtService
    {
        /// <summary>
        /// Get Catalog_Пользователи
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IReadOnlyList<UserUt>> GetUserUtListAsync(CancellationToken ct = default);        

        /// <summary>
        /// Get AccumulationRegister_ТоварыНаСкладах_Balance
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IReadOnlyList<SkuInventoryUt>> GetStockBalanceUtListAsync(CancellationToken ct = default);
    }
}
