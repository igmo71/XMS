using System.ComponentModel.DataAnnotations.Schema;
using XMS.Integration.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;

public class Document_СписаниеБезналичныхДенежныхСредств_Dto : IIntegrationEvent
{
    public Guid Ref_Key { get; set; }
    public string? Number { get; set; }
    public DateTime Date { get; set; }
    public decimal СуммаДокумента { get; set; }
    public Guid? Автор_Key { get; set; }
    public Guid? КСЗ_КатегорияЗатрат_Key { get; set; }
    public Guid? СтатьяДвиженияДенежныхСредств_Key { get; set; }
    public string? ХозяйственнаяОперация { get; set; }
    public string? НазначениеПлатежа { get; set; }
    public string? Комментарий { get; set; }

    // TODO: Кастыль
    [NotMapped] public string? DataVersion { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    [NotMapped] public bool DeletionMark { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public static Document_СписаниеБезналичныхДенежныхСредств_Dto From(Document_СписаниеБезналичныхДенежныхСредств source) =>
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
            НазначениеПлатежа = source.НазначениеПлатежа,
            Комментарий = source.Комментарий
        };
}
