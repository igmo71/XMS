using Microsoft.EntityFrameworkCore;
using XMS.Integration.OneC;
using XMS.Integration.OneC.Ut.Features.Document_РеализацияТоваровУслуг_Feature;

namespace XMS.Infrastructure.Data.EntityConfigurations.OneC;

internal class Document_РеализацияТоваровУслуг_EntityConfiguration
        : IEntityTypeConfiguration<Document_РеализацияТоваровУслуг>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Document_РеализацияТоваровУслуг> builder)
    {
        builder.ToTable("1c_ut_Document_РеализацияТоваровУслуг");

        builder.HasKey(e => e.Ref_Key);

        builder.Property(e => e.СуммаДокумента).HasPrecision(18, 2);

        builder.Property(e => e.DataVersion).HasMaxLength(OneCSettings.CODE);
        builder.Property(e => e.Number).HasMaxLength(OneCSettings.CODE);
        builder.Property(e => e.ХозяйственнаяОперация).HasMaxLength(OneCSettings.VALUE);
    }
}
