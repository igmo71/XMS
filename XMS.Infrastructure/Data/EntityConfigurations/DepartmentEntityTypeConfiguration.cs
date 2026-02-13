using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Domain.Models;

namespace XMS.Infrastructure.Data.EntityConfigurations
{
    public class DepartmentEntityTypeConfiguration : BaseEntityTypeConfiguration<Department>
    {
        public override void Configure(EntityTypeBuilder<Department> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).HasMaxLength(AppSettings.MaxLength.NAME);
            builder.HasOne(x => x.Parent).WithMany(x => x.Children).HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
