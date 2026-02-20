using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Domain.Models;

namespace XMS.Infrastructure.Data.EntityConfigurations
{
    public class SkuInventoryUtEntityTypeConfiguration : BaseEntityTypeConfiguration<SkuInventoryUt>
    {
        public override void Configure(EntityTypeBuilder<SkuInventoryUt> builder)
        {
            base.Configure(builder);

            builder.HasIndex(x => new { x.ScuId, x.WarehouseId }).IsUnique();
            builder.Property(x => x.QuantityOnHand).HasPrecision(18, 3);
        }
    }
}
