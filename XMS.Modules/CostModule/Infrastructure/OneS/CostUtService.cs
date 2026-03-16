using XMS.Application.Common.Integration;
using XMS.Modules.CostModule.Abstractions;
using XMS.Modules.CostModule.Domain;
using XMS.Modules.CostModule.Infrastructure.OneS.Models;

namespace XMS.Modules.CostModule.Infrastructure.OneS
{
    internal class CostUtService(CostUtClient client) : ICostUtService
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
    }
}
