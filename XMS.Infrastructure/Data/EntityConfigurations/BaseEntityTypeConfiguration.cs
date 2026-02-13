using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Domain.Abstractions;

namespace XMS.Infrastructure.Data.EntityConfigurations
{
    public abstract class BaseEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
