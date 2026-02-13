using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Domain.Models;

namespace XMS.Infrastructure.Data.EntityConfigurations
{
    public class CityEntityTypeConfiguration : BaseEntityTypeConfiguration<City>
    {
        public override void Configure(EntityTypeBuilder<City> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).HasMaxLength(AppSettings.MaxLength.NAME);
        }
    }
}
