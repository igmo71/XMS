using XMS.Integration.OneS.Abstractions;
using XMS.Integration.OneS.Zup.Domain;
using XMS.Integration.OneS.Zup.Infrastructure;

namespace XMS.Integration.OneS.Zup.Application
{
    public class ZupService(ZupClient client) : IZupService
    {
        public async Task<Catalog_Сотрудники[]> GetCatalog_Сотрудники(CancellationToken ct = default)
        {
            var result = await client.GetValueAsync<RootObject<Catalog_Сотрудники>>(Catalog_Сотрудники.Uri, ct);

            return result?.Value ?? [];
        }
    }
}
