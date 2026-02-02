using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Core;
using XMS.Modules.Employees.Domain;

namespace XMS.Modules.Employees.Infrastructure.EntityConfigurations
{
    public class UserUtEntityTypeConfiguration : BaseNamedEntityTypeConfiguration<UserUt>
    {
        public override void Configure(EntityTypeBuilder<UserUt> builder)
        {
            base.Configure(builder);
        }
    }
}
