using XMS.Domain.Models;
using XMS.Integration.OneC.Abstractions;
using XMS.Integration.OneC.Ut.Models;

namespace XMS.Integration.OneC.Ut
{
    internal class UtService(UtClient utClient) : IOneCUtService
    {
        public async Task<IReadOnlyList<AccumulationRegister_ТоварыНаСкладах_Balance>> GetAccumulationRegister_ТоварыНаСкладах_Balance_Async(CancellationToken ct = default)
        {
            var rootObject = await utClient.GetValueAsync<RootObject<AccumulationRegister_ТоварыНаСкладах_Balance>>(AccumulationRegister_ТоварыНаСкладах_Balance.Uri, ct);

            var result = rootObject?.Value?.ToList();

            return result ?? [];
        }

        public async Task<IReadOnlyList<Catalog_СтатьиДвиженияДенежныхСредств>> GetCatalog_СтатьиДвиженияДенежныхСредств_Async(CancellationToken ct = default)
        {
            var rootObject = await utClient.GetValueAsync<RootObject<Catalog_СтатьиДвиженияДенежныхСредств>>(Catalog_СтатьиДвиженияДенежныхСредств.Uri, ct);

            var result = rootObject?.Value?.ToList();

            return result ?? [];
        }

       
        

        public async Task<IReadOnlyList<UserUt>> FetchUserUtListAsync(CancellationToken ct = default)
        {
            var rootObject = await utClient.GetValueAsync<RootObject<Catalog_Пользователи>>(Catalog_Пользователи.Uri, ct);

            var result = rootObject?.Value?.Select(x => new UserUt
            {
                Id = x.Ref_Key,
                Name = x.Description ?? string.Empty,
                IsDeleted = x.DeletionMark
            }).ToList();

            return result ?? [];
        }
    }
}
