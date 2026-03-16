using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Modules.CostModule.Domain;

namespace XMS.Infrastructure.Data.EntityConfigurations
{
    public class CashFlowCostEntityTypeConfiguration : BaseEntityTypeConfiguration<CashFlowCost>
    {
        public override void Configure(EntityTypeBuilder<CashFlowCost> builder)
        {
            base.Configure(builder);

            builder.ToTable("CashFlowCost");

            builder.HasOne(e => e.CashFlowItem)
                .WithMany()
                .HasForeignKey(e => e.CashFlowItemId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.CostCategoryItem)
                 .WithMany()
                 .HasForeignKey(e => e.CostCategoryItemId)
                 .HasPrincipalKey(e => e.Id)
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
