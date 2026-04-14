namespace XMS.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature;

public class Document_РасходныйКассовыйОрдер_Dto
{
    public Guid Ref_Key { get; set; }
    public string? Number { get; set; }
    public DateTime Date { get; set; }
    public decimal СуммаДокумента { get; set; }
    public Guid? Автор_Key { get; set; }
    public Guid? КСЗ_КатегорияЗатрат_Key { get; set; }
    public Guid? СтатьяДвиженияДенежныхСредств_Key { get; set; }
    public string? ХозяйственнаяОперация { get; set; }
    public string? Комментарий { get; set; }

    public static Document_РасходныйКассовыйОрдер_Dto From(Document_РасходныйКассовыйОрдер source) =>
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
