using XMS.Web.Integration.OneS.Buh.Domain;

namespace XMS.Web.Integration.OneS.Abstractions
{
    public interface IBuhService
    {
        Task<Catalog_Сотрудники[]> GetCatalog_Сотрудники(CancellationToken ct = default);
    }
}
