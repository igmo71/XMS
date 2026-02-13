using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Domain.Models;

namespace XMS.Infrastructure.Data.EntityConfigurations
{
    public class EmployeeZupEntityTypeConfiguration : BaseEntityTypeConfiguration<EmployeeZup>
    {
        public override void Configure(EntityTypeBuilder<EmployeeZup> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).HasMaxLength(AppSettings.MaxLength.NAME);
            builder.Property(x => x.Code).HasMaxLength(AppSettings.MaxLength.GUID);
        }
    }
}
