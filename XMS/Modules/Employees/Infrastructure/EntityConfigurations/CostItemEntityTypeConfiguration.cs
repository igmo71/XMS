using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Modules.Employees.Domain;

namespace XMS.Modules.Employees.Infrastructure.EntityConfigurations
{
    public class CostItemEntityTypeConfiguration : BaseNamedEntityTypeConfiguration<CostItem>
    {
        public override void Configure(EntityTypeBuilder<CostItem> builder)
        {
            base.Configure(builder);
        }
    }
}
