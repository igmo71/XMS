using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Integration.OneC;
using XMS.Integration.OneC.Ut.Features.Document_ЗаказКлиента_Feature;

namespace XMS.Infrastructure.Data.EntityConfigurations.OneC;

internal class Document_ЗаказКлиента_EntityConfiguration : IEntityTypeConfiguration<Document_ЗаказКлиента>
{
    public void Configure(EntityTypeBuilder<Document_ЗаказКлиента> builder)
    {
        builder.ToTable("1c_ut_Document_ЗаказКлиента");

        builder.HasKey(e => e.Ref_Key);

        builder.Property(e => e.СуммаДокумента).HasPrecision(18, 2);

        builder.Property(e => e.DataVersion).HasMaxLength(OneCSettings.CODE);
        builder.Property(e => e.Number).HasMaxLength(OneCSettings.CODE);
        builder.Property(e => e.ХозяйственнаяОперация).HasMaxLength(OneCSettings.VALUE);
    }
}
