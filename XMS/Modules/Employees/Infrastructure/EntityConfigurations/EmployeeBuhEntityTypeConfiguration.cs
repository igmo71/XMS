using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Core;
using XMS.Modules.Employees.Domain;

namespace XMS.Modules.Employees.Infrastructure.EntityConfigurations
{
    public class EmployeeBuhEntityTypeConfiguration : BaseNamedEntityTypeConfiguration<EmployeeBuh>
    {
        public override void Configure(EntityTypeBuilder<EmployeeBuh> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Code).HasMaxLength(AppSettings.MaxLength.GUID);
        }
    }
}
