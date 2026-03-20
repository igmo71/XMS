using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Integration.OneC.Ut.Models;

namespace XMS.Infrastructure.Data.EntityConfigurations.OneC
{
    internal class Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа_EntityConfiguration
        : IEntityTypeConfiguration<Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа>
    {
        public void Configure(EntityTypeBuilder<Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа> builder)
        {
            builder.ToTable("1c_ut_Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа");

            builder.HasKey(e => new { e.Ref_Key, e.LineNumber });


            builder.Property(e => e.Сумма).HasPrecision(18, 2);
            builder.Property(e => e.СуммаВзаиморасчетов).HasPrecision(18, 2);
        }
    }
}
