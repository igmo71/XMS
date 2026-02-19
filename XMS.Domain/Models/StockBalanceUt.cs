using XMS.Domain.Abstractions;

namespace XMS.Domain.Models
{
    public class StockBalanceUt : BaseEntity
    {
        public Guid NomenclatureId { get; set; }
        public Guid WarehouseId { get; set; }
        public decimal AvailableBalance { get; set; }
    }
}
