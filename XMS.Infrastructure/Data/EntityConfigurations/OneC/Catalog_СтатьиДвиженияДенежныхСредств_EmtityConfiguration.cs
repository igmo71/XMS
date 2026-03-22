using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Integration.OneC.Ut.Features.Catalog_СтатьиДвиженияДенежныхСредств_Feature;

namespace XMS.Infrastructure.Data.EntityConfigurations.OneC
{
    internal class Catalog_СтатьиДвиженияДенежныхСредств_EmtityConfiguration
        : IEntityTypeConfiguration<Catalog_СтатьиДвиженияДенежныхСредств>
    {
        public void Configure(EntityTypeBuilder<Catalog_СтатьиДвиженияДенежныхСредств> builder)
        {
            builder.ToTable("1c_ut_Catalog_СтатьиДвиженияДенежныхСредств");

            builder.HasKey(e => e.Ref_Key);
        }
    }
}
