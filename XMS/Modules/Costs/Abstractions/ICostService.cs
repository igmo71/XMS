using XMS.Modules.Costs.Domain;

namespace XMS.Modules.Costs.Abstractions
{
    public interface ICostService
    {
        Task<List<CostCategory>> GetCategoryTreeAsync();
    }
}
