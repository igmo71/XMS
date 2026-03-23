using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature;

namespace XMS.Infrastructure.Data.EntityConfigurations.OneC
{
    internal class Catalog_Партнеры_EntityConfiguration : IEntityTypeConfiguration<Catalog_Партнеры>
    {
        public void Configure(EntityTypeBuilder<Catalog_Партнеры> builder)
        {
            builder.ToTable("1c_ut_Catalog_Партнеры");

            builder.HasKey(e => e.Ref_Key);
        }
    }
}
