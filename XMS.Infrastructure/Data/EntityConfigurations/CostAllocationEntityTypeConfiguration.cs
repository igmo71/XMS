using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Integrations.OneC.Common;
using XMS.Modules.CostModule.Domain;

namespace XMS.Infrastructure.Data.EntityConfigurations;

public class CostAllocationEntityTypeConfiguration : BaseEntityTypeConfiguration<CostAllocation>
{
    public override void Configure(EntityTypeBuilder<CostAllocation> builder)
    {
        base.Configure(builder);

        builder.ToTable("CostAllocations");

        builder.Property(e => e.TotalAmount).HasPrecision(18, 2);

        builder.HasOne(e => e.CostCategory).WithMany()
            .HasForeignKey(e => e.CostCategoryId).HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.CostItem).WithMany()
            .HasForeignKey(e => e.CostItemId).HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.Author).WithMany()
            .HasForeignKey(e => e.AuthorId).HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(e => e.Department).WithMany()
            .HasForeignKey(e => e.DepartmentId).HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.Location).WithMany()
            .HasForeignKey(e => e.LocationId).HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.City).WithMany()
            .HasForeignKey(e => e.CityId).HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Property(e => e.Number).HasMaxLength(OneCSettings.CODE);
        builder.Property(e => e.BusinessOperation).HasMaxLength(OneCSettings.VALUE);
        builder.Property(e => e.PaymentPurpose).HasMaxLength(OneCSettings.COMMENT);
        builder.Property(e => e.Comment).HasMaxLength(OneCSettings.COMMENT);
    }
}
