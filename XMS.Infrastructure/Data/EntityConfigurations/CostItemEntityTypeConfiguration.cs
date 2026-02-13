using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Domain.Models;

namespace XMS.Infrastructure.Data.EntityConfigurations
{
    public class CostItemEntityTypeConfiguration : BaseEntityTypeConfiguration<CostItem>
    {
        public override void Configure(EntityTypeBuilder<CostItem> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).HasMaxLength(AppSettings.MaxLength.NAME);

            //builder
            //    .HasMany(e => e.Categories)
            //    .WithMany(e => e.Items)
            //    .UsingEntity<CostCategoryItem>(
            //        r => r.HasOne<CostCategory>().WithMany(e => e.CategoryItems).HasForeignKey(e => e.CategoryId),
            //        l => l.HasOne<CostItem>().WithMany(e => e.CategoryItems).HasForeignKey(e => e.ItemId));
        }
    }
}
