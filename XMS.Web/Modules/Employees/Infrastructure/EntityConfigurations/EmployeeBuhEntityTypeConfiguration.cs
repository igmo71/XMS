using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Web.Core;
using XMS.Web.Modules.Employees.Domain;

namespace XMS.Web.Modules.Employees.Infrastructure.EntityConfigurations
{
    public class EmployeeBuhEntityTypeConfiguration : BaseEntityTypeConfiguration<EmployeeBuh>
    {
        public override void Configure(EntityTypeBuilder<EmployeeBuh> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).HasMaxLength(AppSettings.MaxLength.NAME);
            builder.Property(x => x.Code).HasMaxLength(AppSettings.MaxLength.GUID);
        }
    }
}
