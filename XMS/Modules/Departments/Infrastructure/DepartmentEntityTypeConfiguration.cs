using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Core;
using XMS.Modules.Departments.Domain;

namespace XMS.Modules.Departments.Infrastructure
{
    public class DepartmentEntityTypeConfiguration : BaseNamedEntityTypeConfiguration<Department>
    {
        public override void Configure(EntityTypeBuilder<Department> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.Parent).WithMany(x => x.Children).HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
