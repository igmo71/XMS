using System.ComponentModel.DataAnnotations.Schema;


using XMS.Integration.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature;

public record Document_РасходныйКассовыйОрдер_Deleted : IIntegrationEvent
{
    public Guid Ref_Key { get; init; }
    public string? Number { get; init; }
    public DateTime Date { get; init; }
    public decimal СуммаДокумента { get; init; }
    public Guid? Автор_Key { get; init; }
    public Guid? КСЗ_КатегорияЗатрат_Key { get; init; }
    public Guid? СтатьяДвиженияДенежныхСредств_Key { get; init; }
    public string? ХозяйственнаяОперация { get; init; }
    public string? Комментарий { get; init; }

    // TODO: Кастыть...
    [NotMapped] public string? DataVersion { get; init; } = null;
    [NotMapped] public bool DeletionMark { get; init; } = false;

    public static Document_РасходныйКассовыйОрдер_Deleted From(Document_РасходныйКассовыйОрдер source) =>
        new()
        {
            Ref_Key = source.Ref_Key,
            Date = source.Date,
            Number = source.Number,
            СуммаДокумента = source.СуммаДокумента,
            Автор_Key = source.Автор_Key,
            КСЗ_КатегорияЗатрат_Key = source.КСЗ_КатегорияЗатрат_Key,
            СтатьяДвиженияДенежныхСредств_Key = source.СтатьяДвиженияДенежныхСредств_Key,
            ХозяйственнаяОперация = source.ХозяйственнаяОперация,
            Комментарий = source.Комментарий
        };
}
