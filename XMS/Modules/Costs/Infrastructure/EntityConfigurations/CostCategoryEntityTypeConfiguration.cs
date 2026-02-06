using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Core;
using XMS.Modules.Costs.Domain;

namespace XMS.Modules.Costs.Infrastructure.EntityConfigurations
{
    public class CostCategoryEntityTypeConfiguration : BaseNamedEntityTypeConfiguration<CostCategory>
    {
        public override void Configure(EntityTypeBuilder<CostCategory> builder)
        {
            base.Configure(builder);

            builder
                .HasMany(e => e.Items)
                .WithMany(e => e.Categories)
                .UsingEntity<CostCategoryItem>(
                    r => r.HasOne<CostItem>().WithMany(e => e.CategoryItems).HasForeignKey(e => e.ItemId),
                    l => l.HasOne<CostCategory>().WithMany(e => e.CategoryItems).HasForeignKey(e => e.CategoryId));

            builder.HasOne(e => e.Employee).WithMany().HasForeignKey(e => e.EmployeeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Department).WithMany().HasForeignKey(e => e.DepartmentId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
