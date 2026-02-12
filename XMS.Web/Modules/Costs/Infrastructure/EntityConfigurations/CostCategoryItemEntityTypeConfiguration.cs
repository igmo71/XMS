using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Web.Modules.Costs.Domain;

namespace XMS.Web.Modules.Costs.Infrastructure.EntityConfigurations
{
    public class CostCategoryItemEntityTypeConfiguration : IEntityTypeConfiguration<CostCategoryItem>
    {
        public void Configure(EntityTypeBuilder<CostCategoryItem> builder)
        {
            builder.HasKey(x => new { x.CategoryId, x.ItemId });

            builder.HasOne(e => e.Category)
                .WithMany(e => e.CategoryItems) 
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Item)
                .WithMany(e => e.CategoryItems) 
                .HasForeignKey(e => e.ItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
