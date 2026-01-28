using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Modules.Employees.Domain;

namespace XMS.Modules.Employees.Infrastructure.EntityConfigurations
{
    public class PostEntityTypeConfiguration : BaseEntityTypeConfiguration<Post>
    {
        public override void Configure(EntityTypeBuilder<Post> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Name).HasMaxLength(AppSettings.MaxLength.NAME);
        }
    }
}
