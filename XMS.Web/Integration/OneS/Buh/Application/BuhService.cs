using XMS.Web.Integration.OneS.Abstractions;
using XMS.Web.Integration.OneS.Buh.Domain;
using XMS.Web.Integration.OneS.Buh.Infrastructure;

namespace XMS.Web.Integration.OneS.Buh.Application
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
