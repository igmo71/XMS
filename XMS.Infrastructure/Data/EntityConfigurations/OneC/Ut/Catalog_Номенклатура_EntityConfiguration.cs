using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Integrations.OneC.Common;

namespace XMS.Infrastructure.Data.EntityConfigurations.OneC.Ut;

internal class Catalog_Номенклатура_EntityConfiguration : IEntityTypeConfiguration<Catalog_Номенклатура>
{
    public void Configure(EntityTypeBuilder<Catalog_Номенклатура> builder)
    {
        builder.ToTable("1c_ut_Catalog_Номенклатура");

        builder.HasKey(e => e.Ref_Key);

        builder.Property(e => e.DataVersion).HasMaxLength((OneCSettings.CODE));
        builder.Property(e => e.Code).HasMaxLength((OneCSettings.CODE));
        builder.Property(e => e.Description).HasMaxLength((OneCSettings.DESCRIPTION));
    }
}
