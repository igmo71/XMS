using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Domain.Models;

namespace XMS.Infrastructure.Data.EntityConfigurations
{
    public class JobTitleEntityTypeConfiguration : BaseEntityTypeConfiguration<JobTitle>
    {
        public override void Configure(EntityTypeBuilder<JobTitle> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).HasMaxLength(AppSettings.MaxLength.NAME);
        }
    }
}
