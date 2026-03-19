using System.Security.Cryptography;
using XMS.Integration.OneC.Abstractions;
using XMS.Integration.OneC.Ut.Models;

namespace XMS.Integration.OneC.Ut
{
    internal class UtService(UtClient client) : IOneCUtService
    {
        /// <summary>
        /// Get Catalog_Пользователи
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<UserUt>> GetUserUtListAsync(CancellationToken ct = default)
        {
            var rootObject = await client.GetValueAsync<RootObject<Catalog_Пользователи>>(Catalog_Пользователи.Uri, ct);

            var result = rootObject?.Value?.Select(x => new UserUt
            {
                Id = x.Ref_Key,
                Name = x.Description ?? string.Empty,
                IsDeleted = x.DeletionMark
            }).ToList();

            return result ?? [];
        }

        /// <summary>
        /// Get AccumulationRegister_ТоварыНаСкладах_Balance
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<SkuInventoryUt>> GetStockBalanceUtListAsync(CancellationToken ct = default)
        {
            var rootObject = await client.GetValueAsync<RootObject<AccumulationRegister_ТоварыНаСкладах_Balance>>(AccumulationRegister_ТоварыНаСкладах_Balance.Uri, ct);

            var result = rootObject?.Value?.Select(x => new SkuInventoryUt
            {
                Id = CreateStockBalanceId(x.Номенклатура_Key, x.Склад_Key),
                ScuId = x.Номенклатура_Key,
                WarehouseId = x.Склад_Key,
                QuantityOnHand = x.ВНаличииBalance
            }).ToList();

            return result ?? [];
        }

        private static Guid CreateStockBalanceId(Guid nomenclatureId, Guid warehouseId)
        {
            Span<byte> input = stackalloc byte[32];
            nomenclatureId.ToByteArray().CopyTo(input[..16]);
            warehouseId.ToByteArray().CopyTo(input[16..]);

            Span<byte> hash = stackalloc byte[32];
            SHA256.HashData(input, hash);

            return new Guid(hash[..16]);
        }
    }
}
