using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Modules.CostModule.Domain.OneS;

namespace XMS.Infrastructure.Data.EntityConfigurations.OneS
{
    internal class Document_СписаниеБезналичныхДенежныхСредств_EntityConfiguration
        : IEntityTypeConfiguration<Document_СписаниеБезналичныхДенежныхСредств>
    {
        public void Configure(EntityTypeBuilder<Document_СписаниеБезналичныхДенежныхСредств> builder)
        {
            builder.ToTable(nameof(Document_СписаниеБезналичныхДенежныхСредств) );

            builder.HasKey(e => e.Ref_Key);

            builder.HasMany(e => e.РасшифровкаПлатежа).WithOne()
                .HasForeignKey(e => e.Ref_Key).HasPrincipalKey(e => e.Ref_Key)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
