using System.Security.Cryptography;
using XMS.Application.Abstractions.Integration;
using XMS.Application.Common.Integration;
using XMS.Domain.Models;
using XMS.Infrastructure.Integration.OneS.Ut.Models;

namespace XMS.Infrastructure.Integration.OneS.Ut
{
    internal class UtService(UtClient client) : IOneSUtService
    {
        /// <summary>
        /// Get Catalog_СтатьиДвиженияДенежныхСредств
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<CashFlowItem>> GetCashFlowItemListAsync(CancellationToken ct = default)
        {
            var rootObject = await client.GetValueAsync<RootObject<Catalog_СтатьиДвиженияДенежныхСредств>>(Catalog_СтатьиДвиженияДенежныхСредств.Uri, ct);

            var result = rootObject?.Value?.Select(x => new CashFlowItem
            {
                Id = x.Ref_Key,
                Name = x.Description ?? string.Empty,
                ParentId = x.Parent_Key,
                Code = x.Code,
                IsFolder = x.IsFolder,
                IsDeleted = x.DeletionMark
            }).ToList();

            return result ?? [];
        }

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
