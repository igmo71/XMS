using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Core.Abstractions;

namespace XMS.Core
{
    public class BaseNamedEntityTypeConfiguration<T> : BaseEntityTypeConfiguration<T> where T : BaseEntity, IHasName
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).HasMaxLength(AppSettings.MaxLength.NAME);
        }
    }
}
