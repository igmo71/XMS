using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Integrations.OneC.Common;

namespace XMS.Infrastructure.Data.EntityConfigurations.OneC.Ut;

internal class Document_РасходныйКассовыйОрдер_EntityConfiguration : IEntityTypeConfiguration<Document_РасходныйКассовыйОрдер>
{
    public void Configure(EntityTypeBuilder<Document_РасходныйКассовыйОрдер> builder)
    {
        builder.ToTable("1c_ut_Document_РасходныйКассовыйОрдер");

        builder.HasKey(e => e.Ref_Key);

        builder.Property(e => e.СуммаДокумента).HasPrecision(18, 2);

        builder.Property(e => e.DataVersion).HasMaxLength(OneCSettings.CODE);
        builder.Property(e => e.Number).HasMaxLength(OneCSettings.CODE);
        builder.Property(e => e.ЗаявкаНаРасходованиеДенежныхСредств_Type).HasMaxLength(OneCSettings.VALUE);
        builder.Property(e => e.ДокументОснование_Type).HasMaxLength(OneCSettings.VALUE);
        builder.Property(e => e.Договор_Type).HasMaxLength(OneCSettings.VALUE);
        builder.Property(e => e.ХозяйственнаяОперация).HasMaxLength(OneCSettings.VALUE);
        builder.Property(e => e.Выдать).HasMaxLength(OneCSettings.DESCRIPTION);
        builder.Property(e => e.Основание).HasMaxLength(OneCSettings.DESCRIPTION);
        builder.Property(e => e.Приложение).HasMaxLength(OneCSettings.DESCRIPTION);
        builder.Property(e => e.ПоДокументу).HasMaxLength(OneCSettings.DESCRIPTION);
        builder.Property(e => e.НалогообложениеНДС).HasMaxLength(OneCSettings.VALUE);
        builder.Property(e => e.Комментарий).HasMaxLength(OneCSettings.COMMENT);

        builder.HasMany(e => e.РасшифровкаПлатежа).WithOne()
            .HasForeignKey(e => e.Ref_Key).HasPrincipalKey(e => e.Ref_Key)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
