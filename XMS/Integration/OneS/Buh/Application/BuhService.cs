using XMS.Integration.OneS.Abstractions;
using XMS.Integration.OneS.Buh.Domain;
using XMS.Integration.OneS.Ut.Infrastructure;

namespace XMS.Integration.OneS.Buh.Application
{
    public class BuhService(BuhClient buhClient) : IBuhService
    {
        public async Task<Catalog_Сотрудники[]> GetCatalogAsinc()
        {
            var result = await buhClient.GetValue<RootObject<Catalog_Сотрудники>>(Catalog_Сотрудники.Uri);

            return result?.Value ?? [];
        }
    }
}
