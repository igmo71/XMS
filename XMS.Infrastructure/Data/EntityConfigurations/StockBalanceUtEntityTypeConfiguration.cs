using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Domain.Models;

namespace XMS.Infrastructure.Data.EntityConfigurations
{
    public class StockBalanceUtEntityTypeConfiguration : BaseEntityTypeConfiguration<StockBalanceUt>
    {
        public override void Configure(EntityTypeBuilder<StockBalanceUt> builder)
        {
            base.Configure(builder);

            builder.HasIndex(x => new { x.NomenclatureId, x.WarehouseId }).IsUnique();
            builder.Property(x => x.AvailableBalance).HasPrecision(18, 3);
        }
    }
}
