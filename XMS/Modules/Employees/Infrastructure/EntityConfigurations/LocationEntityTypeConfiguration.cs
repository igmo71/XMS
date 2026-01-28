using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Modules.Employees.Domain;

namespace XMS.Modules.Employees.Infrastructure.EntityConfigurations
{
    public class LocationEntityTypeConfiguration : BaseNamedEntityTypeConfiguration<Location>
    {
        public override void Configure(EntityTypeBuilder<Location> builder)
        {
            base.Configure(builder);
        }
    }
}
