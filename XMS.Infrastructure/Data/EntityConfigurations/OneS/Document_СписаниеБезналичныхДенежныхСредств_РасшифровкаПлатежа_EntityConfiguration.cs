using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Integration.OneC.Ut.Models;
using XMS.Modules.CostModule.Domain.OneS;

namespace XMS.Infrastructure.Data.EntityConfigurations.OneS
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
