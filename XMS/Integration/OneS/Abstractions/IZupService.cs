using XMS.Integration.OneS.Zup.Domain;

namespace XMS.Integration.OneS.Abstractions
{
    public interface IZupService
    {
        Task<Catalog_Сотрудники[]> GetCatalog_Сотрудники();
    }
}
