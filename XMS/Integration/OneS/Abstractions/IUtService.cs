using XMS.Integration.OneS.Ut.Domain;

namespace XMS.Integration.OneS.Abstractions
{
    public interface IUtService
    {
        Task<Catalog_Пользователи[]> GetCatalog_Пользователи(CancellationToken ct = default);
    }
}
