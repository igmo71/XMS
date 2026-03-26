using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Integration.OneC;
using XMS.Integration.OneC.Ut.Features.Catalog_КонтактныеЛицаПартнеров_Feature;

namespace XMS.Infrastructure.Data.EntityConfigurations.OneC;

internal class Catalog_КонтактныеЛицаПартнеров_EntityConfiguration : IEntityTypeConfiguration<Catalog_КонтактныеЛицаПартнеров>
{
    public void Configure(EntityTypeBuilder<Catalog_КонтактныеЛицаПартнеров> builder)
    {
        builder.ToTable("1c_ut_Catalog_КонтактныеЛицаПартнеров");

        builder.HasKey(e => e.Ref_Key);

        builder.Property(e => e.DataVersion).HasMaxLength((OneCSettings.CODE));
        builder.Property(e => e.Description).HasMaxLength((OneCSettings.DESCRIPTION));
    }
}
