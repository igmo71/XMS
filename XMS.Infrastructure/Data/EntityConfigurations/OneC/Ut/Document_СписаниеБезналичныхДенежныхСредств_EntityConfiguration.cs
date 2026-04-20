using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Application.Integration.OneC.Common;

namespace XMS.Infrastructure.Data.EntityConfigurations.OneC.Ut;

internal class Document_СписаниеБезналичныхДенежныхСредств_EntityConfiguration
: IEntityTypeConfiguration<Document_СписаниеБезналичныхДенежныхСредств>
{
    public void Configure(EntityTypeBuilder<Document_СписаниеБезналичныхДенежныхСредств> builder)
    {
        builder.ToTable("1c_ut_Document_СписаниеБезналичныхДенежныхСредств");

        builder.HasKey(e => e.Ref_Key);

        builder.Property(e => e.СуммаДокумента).HasPrecision(18, 2);

        builder.Property(e => e.DataVersion).HasMaxLength(OneCSettings.CODE);
        builder.Property(e => e.Number).HasMaxLength(OneCSettings.CODE);
        builder.Property(e => e.ЗаявкаНаРасходованиеДенежныхСредств_Type).HasMaxLength(OneCSettings.VALUE);
        builder.Property(e => e.ДокументОснование_Type).HasMaxLength(OneCSettings.VALUE);
        builder.Property(e => e.Договор_Type).HasMaxLength(OneCSettings.VALUE);
        builder.Property(e => e.ХозяйственнаяОперация).HasMaxLength(OneCSettings.VALUE);
        builder.Property(e => e.НазначениеПлатежа).HasMaxLength(OneCSettings.COMMENT);
        builder.Property(e => e.НалогообложениеНДС).HasMaxLength(OneCSettings.VALUE);
        builder.Property(e => e.Комментарий).HasMaxLength(OneCSettings.COMMENT);

        builder.HasMany(e => e.РасшифровкаПлатежа).WithOne()
            .HasForeignKey(e => e.Ref_Key).HasPrincipalKey(e => e.Ref_Key)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
