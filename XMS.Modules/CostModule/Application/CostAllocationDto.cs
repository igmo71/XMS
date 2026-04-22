using XMS.Modules.CostModule.Domain;

namespace XMS.Modules.CostModule.Application;

public class CostAllocationDto
{
    public required IEnumerable<CostAllocation> Value { get; set; }
    public int TotalItems { get; set; }

    internal static CostAllocationDto FromEntities(IEnumerable<CostAllocation> items, int totalItems)
    {
        return new CostAllocationDto
        {
            Value = items,
            TotalItems = totalItems
        };
    }
}
