using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Integration.OneC.Common;
using XMS.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature;

namespace XMS.Infrastructure.Data.EntityConfigurations.OneC.Ut;

internal class Document_РасходныйКассовыйОрдер_РасшифровкаПлатежа_EntityConfiguration
: IEntityTypeConfiguration<Document_РасходныйКассовыйОрдер_РасшифровкаПлатежа>
{
    public void Configure(EntityTypeBuilder<Document_РасходныйКассовыйОрдер_РасшифровкаПлатежа> builder)
    {
        builder.ToTable("1c_ut_Document_РасходныйКассовыйОрдер_РасшифровкаПлатежа");

        builder.HasKey(e => new { e.Ref_Key, e.LineNumber });

        builder.Property(e => e.Сумма).HasPrecision(18, 2);
        builder.Property(e => e.СуммаВзаиморасчетов).HasPrecision(18, 2);
        builder.Property(e => e.СуммаНДС).HasPrecision(18, 2);

        builder.Property(e => e.ЗаявкаНаРасходованиеДенежныхСредств_Type).HasMaxLength(OneCSettings.VALUE);
        builder.Property(e => e.СтатьяРасходов_Type).HasMaxLength(OneCSettings.VALUE);
        builder.Property(e => e.Комментарий).HasMaxLength(OneCSettings.COMMENT);
    }
}
