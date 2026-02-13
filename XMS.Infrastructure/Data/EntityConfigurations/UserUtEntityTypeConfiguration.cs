using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Domain.Models;

namespace XMS.Infrastructure.Data.EntityConfigurations
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
