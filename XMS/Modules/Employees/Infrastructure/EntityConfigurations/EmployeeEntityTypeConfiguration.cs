using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Modules.Employees.Domain;

namespace XMS.Modules.Employees.Infrastructure.EntityConfigurations
{
    public class EmployeeEntityTypeConfiguration : BaseEntityTypeConfiguration<Employee>
    {
        public override void Configure(EntityTypeBuilder<Employee> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Name).HasMaxLength(AppSettings.MaxLength.NAME);
            builder.HasOne(x => x.Post).WithMany().HasForeignKey(x => x.PostId);
        }
    }
}
