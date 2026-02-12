using XMS.Web.Integration.OneS.Ut.Domain;

namespace XMS.Web.Integration.OneS.Abstractions
{
    public interface IUtService
    {
        Task<Catalog_Пользователи[]> GetCatalog_Пользователи(CancellationToken ct = default);
    }
}
