using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Modules.CostModule.Domain;

namespace XMS.Infrastructure.Data.EntityConfigurations
{
    public class CashFlowItemEntityTypeConfiguration : BaseEntityTypeConfiguration<CashFlowItem>
    {
        public override void Configure(EntityTypeBuilder<CashFlowItem> builder)
        {
            base.Configure(builder);

            builder.ToTable("CashFlowItems");

            builder.Property(x => x.Name).HasMaxLength(AppSettings.MaxLength.NAME);
            //builder.HasOne(x => x.Parent).WithMany(x => x.Children).HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
