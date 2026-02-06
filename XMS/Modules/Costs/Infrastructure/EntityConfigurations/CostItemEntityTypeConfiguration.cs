using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Core;
using CostItem = XMS.Modules.Costs.Domain.CostItem;

namespace XMS.Modules.Costs.Infrastructure.EntityConfigurations
{
    public class CostItemEntityTypeConfiguration : BaseNamedEntityTypeConfiguration<CostItem>
    {
        public override void Configure(EntityTypeBuilder<CostItem> builder)
        {
            base.Configure(builder);

            //builder
            //    .HasMany(e => e.Categories)
            //    .WithMany(e => e.Items)
            //    .UsingEntity<CostCategoryItem>(
            //        r => r.HasOne<CostCategory>().WithMany(e => e.CategoryItems).HasForeignKey(e => e.CategoryId),
            //        l => l.HasOne<CostItem>().WithMany(e => e.CategoryItems).HasForeignKey(e => e.ItemId));
        }
    }
}
