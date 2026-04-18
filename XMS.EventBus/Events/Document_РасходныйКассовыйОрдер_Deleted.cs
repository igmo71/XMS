using XMS.Core.Abstractions.EventBus;
using XMS.EventBus.Abstractions;

namespace XMS.EventBus.Events;

public record Document_РасходныйКассовыйОрдер_Deleted : IAppEvent
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
}
