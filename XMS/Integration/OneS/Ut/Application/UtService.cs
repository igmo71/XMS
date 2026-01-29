using XMS.Integration.OneS.Abstractions;
using XMS.Integration.OneS.Ut.Domain;
using XMS.Integration.OneS.Ut.Infrastructure;

namespace XMS.Integration.OneS.Ut.Application
{
    public class UtService(UtClient client) : IUtService
    {
        public async Task<Catalog_Пользователи[]> GetCatalog_Пользователи(CancellationToken ct = default)
        {
            var result = await client.GetValueAsync<RootObject<Catalog_Пользователи>>(Catalog_Пользователи.Uri, ct);

            return result?.Value ?? [];
        }
    }
}
