using System.Text.Json.Serialization;

namespace XMS.Application.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature;

public class Document_РасходныйКассовыйОрдер : Document, ISelectable, IAppEvent
{
    public static string? Select => "Ref_Key,DataVersion,DeletionMark,Posted,Number,Date,СуммаДокумента,СтатьяДвиженияДенежныхСредств_Key,Организация_Key,Подразделение_Key,Автор_Key," +
        "Партнер_Key,Контрагент_Key,НаправлениеДеятельности_Key,ОбъектРасчетов_Key,Валюта_Key,ЗаявкаНаРасходованиеДенежныхСредств,ЗаявкаНаРасходованиеДенежныхСредств_Type," +
        "ДокументОснование,ДокументОснование_Type,Договор,Договор_Type,ХозяйственнаяОперация,Выдать,Основание,Приложение,ПоДокументу,НалогообложениеНДС,Комментарий,РасшифровкаПлатежа,КСЗ_КатегорияЗатрат_Key";

    public decimal СуммаДокумента { get; set; }
    public Guid? СтатьяДвиженияДенежныхСредств_Key { get; set; }
    public Guid? Организация_Key { get; set; }
    public Guid? Подразделение_Key { get; set; }
    public Guid? Автор_Key { get; set; }
    public Guid? Партнер_Key { get; set; }
    public Guid? Контрагент_Key { get; set; }
    public Guid? НаправлениеДеятельности_Key { get; set; }
    public Guid? ОбъектРасчетов_Key { get; set; }
    public Guid? Валюта_Key { get; set; }

    [JsonConverter(typeof(EmptyStringToGuidConverter))]
    public Guid? ЗаявкаНаРасходованиеДенежныхСредств { get; set; }
    public string? ЗаявкаНаРасходованиеДенежныхСредств_Type { get; set; }

    [JsonConverter(typeof(EmptyStringToGuidConverter))]
    public Guid? ДокументОснование { get; set; }
    public string? ДокументОснование_Type { get; set; }

    [JsonConverter(typeof(EmptyStringToGuidConverter))]
    public Guid? Договор { get; set; }
    public string? Договор_Type { get; set; }

    public string? ХозяйственнаяОперация { get; set; }
    public string? Выдать { get; set; }
    public string? Основание { get; set; }
    public string? Приложение { get; set; }
    public string? ПоДокументу { get; set; }
    public string? НалогообложениеНДС { get; set; }

    [JsonConverter(typeof(StringTrimConverter))]
    public string? Комментарий { get; set; }

    public Guid? КСЗ_КатегорияЗатрат_Key { get; set; }

    public List<Document_РасходныйКассовыйОрдер_РасшифровкаПлатежа>? РасшифровкаПлатежа { get; set; }

    //public object[] ВыплатаЗаработнойПлаты { get; set; }
    //public object[] ДополнительныеРеквизиты { get; set; }
    //public Guid? ПодотчетноеЛицо_Key { get; set; }
    //public string ОрганизацияПолучатель_Key { get; set; }
    //public string Касса_Key { get; set; }
    //public string КассаПолучатель { get; set; }
    //public string КассаПолучатель_Type { get; set; }
    //public string БанковскийСчет_Key { get; set; }
    //public string РаспоряжениеНаПеремещениеДенежныхСредств_Key { get; set; }
    //public string КассаККМ_Key { get; set; }
    //public bool НеКонтролироватьЗаполнениеЗаявки { get; set; }
    //public string ВалютаКонвертации_Key { get; set; }
    //public int КурсКонвертации { get; set; }
    //public int СуммаКонвертации { get; set; }
    //public string Ведомость { get; set; }
    //public string Руководитель_Key { get; set; }
    //public string ГлавныйБухгалтер_Key { get; set; }
    //public string НомерВедомостиНаВыплатуЗарплаты { get; set; }
    //public DateTime ДатаВедомостиНаВыплатуЗарплаты { get; set; }
    //public bool ОплатаПоЗаявкам { get; set; }
    //public bool ПроводкиПоРаботникам { get; set; }
    //public DateTime ДатаАвансовогоОтчета { get; set; }
    //public string ГруппаФинансовогоУчета_Key { get; set; }
    //public int КратностьКурсаКонвертации { get; set; }
    //public string ИдентификаторДокумента { get; set; }
    //public bool Исправление { get; set; }
    //public string СторнируемыйДокумент_Key { get; set; }
    //public string ИсправляемыйДокумент_Key { get; set; }
    //public string УдалитьКассир_Key { get; set; }
    //public DateTime ПериодРегистрации { get; set; }
    //public string Кассир_Key { get; set; }
}

public class Document_РасходныйКассовыйОрдер_РасшифровкаПлатежа
{
    public Guid Ref_Key { get; set; }

    [JsonConverter(typeof(StringToIntConverter))]
    public int LineNumber { get; set; }
    public decimal Сумма { get; set; }
    public decimal СуммаВзаиморасчетов { get; set; }
    public Guid? ВалютаВзаиморасчетов_Key { get; set; }
    public Guid? СтатьяДвиженияДенежныхСредств_Key { get; set; }
    public Guid? Организация_Key { get; set; }
    public Guid? Подразделение_Key { get; set; }
    public Guid? Партнер_Key { get; set; }
    public Guid? НаправлениеДеятельности_Key { get; set; }
    public Guid? ОбъектРасчетов_Key { get; set; }
    public Guid? СтавкаНДС_Key { get; set; }
    public decimal СуммаНДС { get; set; }

    [JsonConverter(typeof(EmptyStringToGuidConverter))]
    public Guid? ЗаявкаНаРасходованиеДенежныхСредств { get; set; }
    public string? ЗаявкаНаРасходованиеДенежныхСредств_Type { get; set; }

    [JsonConverter(typeof(EmptyStringToGuidConverter))]
    public Guid? СтатьяРасходов { get; set; }
    public string? СтатьяРасходов_Type { get; set; }

    [JsonConverter(typeof(StringTrimConverter))]
    public string? Комментарий { get; set; }

    //public string АналитикаРасходов { get; set; }
    //public string АналитикаРасходов_Type { get; set; }
    //public string ДоговорКредитаДепозита_Key { get; set; }
    //public string ДоговорАренды { get; set; }
    //public string ТипПлатежаПоАренде { get; set; }
    //public string ИдентификаторСтроки { get; set; }
    //public string АналитикаАктивовПассивов { get; set; }
    //public string АналитикаАктивовПассивов_Type { get; set; }
    //public string ДоговорЗаймаСотруднику { get; set; }
    //public string ТипСуммыКредитаДепозита { get; set; }
    //public string НастройкаСчетовУчета { get; set; }
    //public int КурсЧислительВзаиморасчетов { get; set; }
    //public int КурсЗнаменательВзаиморасчетов { get; set; }
    //public DateTime ДатаПогашения { get; set; }
}

