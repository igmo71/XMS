using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Domain.Models;

namespace XMS.Infrastructure.Data.EntityConfigurations
{
    public class CostCategoryItemEntityTypeConfiguration : BaseEntityTypeConfiguration<CostCategoryItem>
    {
        public override void Configure(EntityTypeBuilder<CostCategoryItem> builder)
        {
            base.Configure(builder);

            builder.HasOne(e => e.Category)
                .WithMany(e => e.CategoryItems) 
                .HasForeignKey(e => e.CategoryId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Item)
                .WithMany(e => e.CategoryItems) 
                .HasForeignKey(e => e.ItemId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
