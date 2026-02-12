using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Web.Core;
using XMS.Web.Modules.Employees.Domain;

namespace XMS.Web.Modules.Employees.Infrastructure.EntityConfigurations
{
    public class PostEntityTypeConfiguration : BaseEntityTypeConfiguration<JobTitle>
    {
        public override void Configure(EntityTypeBuilder<JobTitle> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).HasMaxLength(AppSettings.MaxLength.NAME);
        }
    }
}
