using XMS.Integration.OneS.Buh.Domain;

namespace XMS.Integration.OneS.Abstractions
{
    public interface IBuhService
    {
        Task<Catalog_Сотрудники[]> GetCatalog_Сотрудники();
    }
}
