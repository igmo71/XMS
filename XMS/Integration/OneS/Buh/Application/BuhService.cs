using XMS.Integration.OneS.Abstractions;
using XMS.Integration.OneS.Buh.Domain;
using XMS.Integration.OneS.Buh.Infrastructure;

namespace XMS.Integration.OneS.Buh.Application
{
    public class BuhService(BuhClient client) : IBuhService
    {
        public async Task<Catalog_Сотрудники[]> GetCatalog_Сотрудники(CancellationToken ct = default)
        {
            var result = await client.GetValueAsync<RootObject<Catalog_Сотрудники>>(Catalog_Сотрудники.Uri, ct);

            return result?.Value ?? [];
        }
    }
}
