using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Domain.Models;

namespace XMS.Infrastructure.Data.EntityConfigurations
{
    public class LocationEntityTypeConfiguration : BaseEntityTypeConfiguration<Location>
    {
        public override void Configure(EntityTypeBuilder<Location> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).HasMaxLength(AppSettings.MaxLength.NAME);

            builder.HasOne(e => e.Manager).WithMany()
                .HasForeignKey(e => e.ManagerId).HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
