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
            builder.ToTable(nameof(Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа));

            builder.HasKey(e => new { e.Ref_Key, e.LineNumber });
        }
    }
}
