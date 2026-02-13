using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Domain.Models;

namespace XMS.Infrastructure.Data.EntityConfigurations
{
    public class UserAdEntityTypeConfiguration : IEntityTypeConfiguration<UserAd>
    {
        public void Configure(EntityTypeBuilder<UserAd> builder)
        {
            builder.HasKey(x => x.Sid);
            builder.Property(x => x.Sid).HasMaxLength(AppSettings.MaxLength.SID);
            builder.Property(x => x.Name).HasMaxLength(AppSettings.MaxLength.NAME);
            builder.Property(x => x.Login).HasMaxLength(AppSettings.MaxLength.NAME);
            builder.Property(x => x.Title).HasMaxLength(AppSettings.MaxLength.NAME);
            builder.Property(x => x.Department).HasMaxLength(AppSettings.MaxLength.NAME);
            builder.Property(x => x.Manager).HasMaxLength(AppSettings.MaxLength.SID);
            builder.Property(x => x.DistinguishedName).HasMaxLength(450);
        }
    }
}
