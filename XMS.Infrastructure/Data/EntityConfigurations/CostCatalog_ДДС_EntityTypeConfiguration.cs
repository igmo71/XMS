using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Modules.CostModule.Domain;

namespace XMS.Infrastructure.Data.EntityConfigurations;

public class CostCatalog_ДДС_EntityTypeConfiguration : BaseEntityTypeConfiguration<CostCatalog_ДДС>
{
    public override void Configure(EntityTypeBuilder<CostCatalog_ДДС> builder)
    {
        base.Configure(builder);

        builder.ToTable("CostCatalog_ДДС");

        builder.HasOne(e => e.CostCategoryItem)
             .WithMany()
             .HasForeignKey(e => e.CostCategoryItemId)
             .HasPrincipalKey(e => e.Id)
             .OnDelete(DeleteBehavior.Cascade);
    }
}
