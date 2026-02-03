using XMS.Modules.Costs.Domain;
using XMS.Modules.Employees.Abstractions;

namespace XMS.Modules.Costs.Abstractions
{
    public interface ICostItemService : ICrudService<CostItem>
    {
        Task CreateAsync(CostItem item, ICollection<CostCategory> categories, CancellationToken ct = default);
    }
}
