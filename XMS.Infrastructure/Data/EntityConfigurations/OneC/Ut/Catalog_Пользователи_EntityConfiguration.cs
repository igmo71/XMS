using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Application.Integration.OneC.Common;

namespace XMS.Infrastructure.Data.EntityConfigurations.OneC.Ut;

internal class Catalog_Пользователи_EntityConfiguration : IEntityTypeConfiguration<Catalog_Пользователи>
{
    public void Configure(EntityTypeBuilder<Catalog_Пользователи> builder)
    {
        builder.ToTable("1c_ut_Catalog_Пользователи");

        builder.HasKey(e => e.Ref_Key);

        builder.Property(e => e.DataVersion).HasMaxLength((OneCSettings.CODE));
        builder.Property(e => e.Description).HasMaxLength((OneCSettings.DESCRIPTION));
    }
}
