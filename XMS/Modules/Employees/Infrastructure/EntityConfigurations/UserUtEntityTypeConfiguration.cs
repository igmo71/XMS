using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Core;
using XMS.Modules.Employees.Domain;

namespace XMS.Modules.Employees.Infrastructure.EntityConfigurations
{
    public class UserUtEntityTypeConfiguration : BaseEntityTypeConfiguration<UserUt>
    {
        public override void Configure(EntityTypeBuilder<UserUt> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).HasMaxLength(AppSettings.MaxLength.NAME);
        }
    }
}
