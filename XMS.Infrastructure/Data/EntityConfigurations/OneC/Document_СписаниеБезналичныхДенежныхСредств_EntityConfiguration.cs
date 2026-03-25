using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;

namespace XMS.Infrastructure.Data.EntityConfigurations.OneC;

internal class Document_СписаниеБезналичныхДенежныхСредств_EntityConfiguration
    : IEntityTypeConfiguration<Document_СписаниеБезналичныхДенежныхСредств>
{
    public void Configure(EntityTypeBuilder<Document_СписаниеБезналичныхДенежныхСредств> builder)
    {
        builder.ToTable("1c_ut_Document_СписаниеБезналичныхДенежныхСредств");

        builder.HasKey(e => e.Ref_Key);

        builder.Property(e => e.СуммаДокумента).HasPrecision(18, 2);

        builder.HasMany(e => e.РасшифровкаПлатежа).WithOne()
            .HasForeignKey(e => e.Ref_Key).HasPrincipalKey(e => e.Ref_Key)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
