using XMS.Domain.Abstractions;

namespace XMS.Domain.Models
{
    public class SkuInventoryUt : BaseEntity
    {
        public Guid ScuId { get; set; }
        public Guid WarehouseId { get; set; }
        public decimal QuantityOnHand { get; set; }
    }
}
