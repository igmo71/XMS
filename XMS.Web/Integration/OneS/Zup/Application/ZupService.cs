using XMS.Web.Integration.OneS.Abstractions;
using XMS.Web.Integration.OneS.Zup.Domain;
using XMS.Web.Integration.OneS.Zup.Infrastructure;

namespace XMS.Web.Integration.OneS.Zup.Application
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
