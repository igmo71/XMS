using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Modules.CostModule.Domain;

namespace XMS.Infrastructure.Data.EntityConfigurations
{
    public class CostCatalogUtEntityTypeConfiguration : BaseEntityTypeConfiguration<CostCatalogUt>
    {
        public override void Configure(EntityTypeBuilder<CostCatalogUt> builder)
        {
            base.Configure(builder);

            builder.ToTable("CostCatalogUt");
            
            builder.HasOne(e => e.CostCategoryItem)
                 .WithMany()
                 .HasForeignKey(e => e.CostCategoryItemId)
                 .HasPrincipalKey(e => e.Id)
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
