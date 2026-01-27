using BuhDomain = XMS.Integration.OneS.Buh.Domain;
using ZupDomain = XMS.Integration.OneS.Zup.Domain;

namespace XMS.Integration.OneS.Abstractions
{
    public interface IOneSService<TCatalog>
    {
        Task<TCatalog[]> GetCatalogAsinc();
    }

    public interface IBuhService : IOneSService<BuhDomain.Catalog_Сотрудники>
    { }

    public interface IZupService : IOneSService<ZupDomain.Catalog_Сотрудники>
    { }
}
